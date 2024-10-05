using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Domain.Services;
using UI.Command;
using UI.Services;
using UI.Services.Abstractions;
using UI.Stores;
using UI.ViewModel.HelperViewModels;

namespace UI.ViewModel.Sales;

internal class PassengerRegistrationViewModel : ViewModelBase, IDisposable
{
    private readonly IMessageBoxService _messageBoxService;
    private readonly IRunRepository _runRepository;
    private readonly IPassportRepository _passportRepository;
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly OrderProcessService _orderProcessService;
    private readonly AccountStore _accountStore;

    private readonly OrderStore _orderStore;
    private readonly NavigationService _navigationService;
    private int _price;
    private int _cash;

    private DateTime _departureDateTime;
    private DateTime _arrivalDateTime;
    private StationViewModel _departureStation;
    private StationViewModel _arrivalStation;
    private RunViewModel _selectedRun;
    private PassengerViewModel _selectedPassenger;

    public ICommand AddPassengerCommand { get; }
    public ICommand DeletePassengerCommand { get; }
    public ICommand DeclineCommand { get; }
    public ICommand CashPaymentCommand { get; }
    public ICommand NoncashPaymentCommand { get; }

    public PassengerRegistrationViewModel(NavigationService navigationService,
        OrderStore orderStore,
        IMessageBoxService messageBoxService,
        AccountStore accountStore,
        IRunRepository runRepository,
        OrderProcessService orderProcessService,
        IPassportRepository passportRepository,
        ITicketTypeRepository ticketTypeRepository)
    {
        _orderStore = orderStore;
        _orderStore.OrderCreated += OnOrderCreated;
        _navigationService = navigationService;
        _messageBoxService = messageBoxService;
        _accountStore = accountStore;
        _runRepository = runRepository;
        _orderProcessService = orderProcessService;
        _passportRepository = passportRepository;
        _ticketTypeRepository = ticketTypeRepository;

        Passengers = new ObservableCollection<PassengerViewModel>();

        CashPaymentCommand = new RelayCommand(CalcPrice, CanSell);
        NoncashPaymentCommand = new RelayCommand(ProcessOrder, () => CanSell() && ValidateCash());
        AddPassengerCommand = new RelayCommand(AddPassenger);
        DeletePassengerCommand = new RelayCommand(DeletePassenger);
        DeclineCommand = new RelayCommand(Decline);
    }

    private void CalcPrice()
    {
        _orderProcessService.Clear();
        Run run = _runRepository.GetById(SelectedRun.Id);
        string cashierName = _accountStore.CurrentAccount.Username;

        foreach (PassengerViewModel item in Passengers)
        {
            TicketType tt = _ticketTypeRepository.GetById(item.SelectedTicketType.Id);
            IdentityDocument document = item.GetDocument();

            bool isExist = false;
            try
            {
                isExist = _passportRepository.IsExist(document);
            }
            catch
            {
                _messageBoxService.ShowMessage($"Неверные паспортные данные {item.Series} {item.Number}"); return;
            }

            try
            {
                _orderProcessService.AddTicket(null,
                    run, cashierName, tt);
            }
            catch (Exception e)
            {
                _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
            }
        }
        Price = _orderProcessService.GetFullPrice();
    }

    private void Decline()
    {
        _navigationService.Navigate<RunSearchViewModel>();
    }

    private void ProcessOrder()
    {
        _orderProcessService.Clear();
        Run run = _runRepository.GetById(SelectedRun.Id);
        string cashierName = _accountStore.CurrentAccount.Username;

        foreach (PassengerViewModel item in Passengers)
        {
            TicketType tt = _ticketTypeRepository.GetById(item.SelectedTicketType.Id);
            IdentityDocument document = item.GetDocument();

            bool isExist = false;
            try
            {
                isExist = _passportRepository.IsExist(document);
            }
            catch
            {
                _messageBoxService.ShowMessage($"Неверные паспортные данные {item.Series} {item.Number}"); return;
            }

            try
            {
                if (!isExist)
                {
                    _passportRepository.Create(document);
                }
                _orderProcessService.AddTicket(_passportRepository.Get(document.Number, document.Series),
                    run, cashierName, tt);
            }
            catch(Exception e)
            {
                _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
            }
        }
        try
        {
            _orderProcessService.PrintTickets();
            _orderProcessService.PrintReceipt(cashierName);
            _messageBoxService.ShowMessage("Оплата прошла успешно");
            _navigationService.Navigate<RunSearchViewModel>();
        }
        catch (Exception e)
        {
            _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
        }
    }

    private void OnOrderCreated(OrderViewModel order)
    {
        DepartureStation = new StationViewModel(order.DepartureStation);
        ArrivalStation = new StationViewModel(order.ArrivalStation);
        int freePlaces = _runRepository.GetFreePlaces(order.SelectedRun.Id);
        SelectedRun = new RunViewModel(order.SelectedRun, freePlaces);
        DepartureDateTime = SelectedRun.DepartureDateTime;
        ArrivalDateTime = SelectedRun.EstimatedArrivalDateTime;
    }

    private void AddPassenger()
    {
        if (Passengers.Count > 0)
        {
            PassengerViewModel viewModel = Passengers.Last();

            if (!ValidatePassenger(viewModel))
                return;
        }

        Passengers.Add(new PassengerViewModel(_ticketTypeRepository) { });
        SelectedPassenger = Passengers.Last();
    }

    private void DeletePassenger()
    {
        if (SelectedPassenger == null)
            return;

        Passengers.Remove(SelectedPassenger);
    }

    private bool CanSell()
    {
        if (Passengers.Count == 0) return false;
        foreach (PassengerViewModel viewModel in Passengers)
            if (ValidatePassenger(viewModel) == false)
                return false;

        return true;
    }

    private bool ValidateCash()
    {
        if (Price <= 0) return false;
        if (Cash <= 0) return false;
        if (Change < 0) return false;

        return true;
    }

    private bool ValidatePassenger(PassengerViewModel passenger)
    {
        if (string.IsNullOrWhiteSpace(passenger.Series) ||
            string.IsNullOrWhiteSpace(passenger.Number) ||
            string.IsNullOrWhiteSpace(passenger.Name) ||
            string.IsNullOrWhiteSpace(passenger.Surname) ||
            string.IsNullOrWhiteSpace(passenger.Patronymic) ||
            passenger.SelectedTicketType == null)
        {
            return false;
        }
        return true;
    }

    public void Dispose()
    {
        _orderStore.OrderCreated -= OnOrderCreated;
    }

    public DateTime DepartureDateTime
    {
        get => _departureDateTime;
        set { _departureDateTime = value; OnPropertyChanged(); }
    }
    public DateTime ArrivalDateTime
    {
        get => _arrivalDateTime;
        set { _arrivalDateTime = value; OnPropertyChanged(); }
    }
    public StationViewModel DepartureStation
    {
        get => _departureStation;
        set { _departureStation = value; OnPropertyChanged(); }
    }
    public StationViewModel ArrivalStation
    {
        get => _arrivalStation;
        set { _arrivalStation = value; OnPropertyChanged(); }
    }
    public RunViewModel SelectedRun
    {
        get => _selectedRun;
        set { _selectedRun = value; OnPropertyChanged(); }
    }

    public ObservableCollection<PassengerViewModel> Passengers { get; set; }

    public PassengerViewModel SelectedPassenger
    {
        get => _selectedPassenger;
        set { _selectedPassenger = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsPassengerSelected)); }
    }

    public bool IsPassengerSelected => SelectedPassenger != null;

    public int Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged();
            OnPropertyChangedByName(nameof(Change));
        }
    }
    public int Cash
    {
        get => _cash;
        set
        {
            _cash = value;
            OnPropertyChanged();
            OnPropertyChangedByName(nameof(Change));
        }
    }
    public int Change => Cash - Price;
}