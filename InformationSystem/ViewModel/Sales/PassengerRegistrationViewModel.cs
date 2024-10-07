using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Services;
using InformationSystem.Services.Abstractions;
using InformationSystem.Stores;
using InformationSystem.ViewModel.HelperViewModels;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Sales;

internal class PassengerRegistrationViewModel : ViewModelBase, IDisposable
{
    private readonly IDbContextFactory<DomainContext> _contextFactory;
        
    private readonly IMessageBoxService _messageBoxService;
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
        OrderProcessService orderProcessService, IDbContextFactory<DomainContext> contextFactory)
    {
        _orderStore = orderStore;
        _orderStore.OrderCreated += OnOrderCreated;
        _navigationService = navigationService;
        _messageBoxService = messageBoxService;
        _accountStore = accountStore;
        _orderProcessService = orderProcessService;
        _contextFactory = contextFactory;

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
        
        using DomainContext context = _contextFactory.CreateDbContext();
        
        Run run = context.Runs.First(r => r.Id == SelectedRun.Id);

        string cashierName = _accountStore.CurrentAccount.Username;

        foreach (PassengerViewModel item in Passengers)
        {
            TicketType tt = context.TicketTypes.First(t => t.Id == item.SelectedTicketType.Id);
            IdentityDocument document = item.GetDocument();

            bool isExist = false;
            try
            {
                isExist = context.TicketTypes.FirstOrDefault(t => t.Id == document.Id) is not null;
            }
            catch
            {
                _messageBoxService.ShowMessage($"Неверные паспортные данные {item.Series} {item.Number}"); return;
            }

            try
            {
                _orderProcessService.AddTicket(null, run, cashierName, tt);
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
        using DomainContext context = _contextFactory.CreateDbContext();
        Run run = context.Runs.First(r => r.Id == SelectedRun.Id);

        string cashierName = _accountStore.CurrentAccount.Username;

        foreach (PassengerViewModel item in Passengers)
        {
            TicketType tt = context.TicketTypes.First(t => t.Id == item.SelectedTicketType.Id);
            IdentityDocument document = item.GetDocument();

            bool isExist = false;
            
            try
            {
                isExist = context.TicketTypes.FirstOrDefault(t => t.Id == document.Id) is not null;
            }
            catch
            {
                _messageBoxService.ShowMessage($"Неверные паспортные данные {item.Series} {item.Number}"); return;
            }

            try
            {
                if (!isExist)
                {
                    context.Passports.Add(document);
                    context.SaveChanges();
                }
                
                _orderProcessService.AddTicket(context.Passports.First(p => p.Id == document.Id),
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
        int freePlaces = 0;
        using (DomainContext context = _contextFactory.CreateDbContext())
        {
            int id = order.SelectedRun.Id;
            int takenPlaces = context.Tickets.Count(o => o.RunId == id);
            int allPlaces = context.Runs
                .Include(o => o.Vehicle).ThenInclude(x => x.VehicleModel)
                .First(o => o.Id == id).Vehicle.VehicleModel.Capacity;
            
            freePlaces = allPlaces - takenPlaces;
        }
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

        //Passengers.Add(new PassengerViewModel(_ticketTypeRepository));
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
        set { _departureDateTime = value; NotifyPropertyChanged(); }
    }
    public DateTime ArrivalDateTime
    {
        get => _arrivalDateTime;
        set { _arrivalDateTime = value; NotifyPropertyChanged(); }
    }
    public StationViewModel DepartureStation
    {
        get => _departureStation;
        set { _departureStation = value; NotifyPropertyChanged(); }
    }
    public StationViewModel ArrivalStation
    {
        get => _arrivalStation;
        set { _arrivalStation = value; NotifyPropertyChanged(); }
    }
    public RunViewModel SelectedRun
    {
        get => _selectedRun;
        set { _selectedRun = value; NotifyPropertyChanged(); }
    }

    public ObservableCollection<PassengerViewModel> Passengers { get; set; }

    public PassengerViewModel SelectedPassenger
    {
        get => _selectedPassenger;
        set { _selectedPassenger = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(IsPassengerSelected)); }
    }

    public bool IsPassengerSelected => SelectedPassenger != null;

    public int Price
    {
        get => _price;
        set
        {
            _price = value;
            NotifyPropertyChanged();
            NotifyPropertyChanged(nameof(Change));
        }
    }
    public int Cash
    {
        get => _cash;
        set
        {
            _cash = value;
            NotifyPropertyChanged();
            NotifyPropertyChanged(nameof(Change));
        }
    }
    public int Change => Cash - Price;
}