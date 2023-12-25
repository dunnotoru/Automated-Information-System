using Domain.Models;
using Domain.RepositoryInterfaces;
using Domain.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.Stores;
using UI.ViewModel.HelperViewModels;
using UI.ViewModel.Sales;

namespace UI.ViewModel
{
    internal class PassengerRegistrationViewModel : ViewModelBase, IDisposable
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IRunRepository _runRepository;
        private readonly IPassportRepository _passportRepository;
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

        public PassengerRegistrationViewModel(NavigationService navigationService, OrderStore orderStore,
            IMessageBoxService messageBoxService, AccountStore accountStore, IRunRepository runRepository, OrderProcessService orderProcessService, IPassportRepository passportRepository)
        {
            _orderStore = orderStore;
            _orderStore.OrderCreated += OnOrderCreated;
            _navigationService = navigationService;
            _messageBoxService = messageBoxService;
            _accountStore = accountStore;
            _runRepository = runRepository;
            _orderProcessService = orderProcessService;
            _passportRepository = passportRepository;

            Passengers = new ObservableCollection<PassengerViewModel>();

            CashPaymentCommand = new RelayCommand(ProcessOrder, () => CanSell() && ValidateCash());
            NoncashPaymentCommand = new RelayCommand(ProcessOrder, CanSell);
            AddPassengerCommand = new RelayCommand(AddPassenger);
            DeletePassengerCommand = new RelayCommand(DeletePassenger);
            DeclineCommand = new RelayCommand(Decline);
        }
        
        private void Decline()
        {
            _navigationService.Navigate<RunSearchViewModel>();
        }

        private void ProcessOrder()
        {
            Run run = _runRepository.GetById(SelectedRun.Id);
            string cashierName = _accountStore.CurrentAccount.Username;
            try
            {
                foreach (PassengerViewModel item in Passengers)
                {
                    IdentityDocument identityDocument = item.GetDocument();
                    try
                    {
                        identityDocument = _passportRepository.Get(identityDocument.Number, identityDocument.Series);
                    }
                    catch
                    {

                    }

                    _orderProcessService.AddTicket(identityDocument, run, cashierName, null);
                }

                _orderProcessService.PrintTickets();
                _orderProcessService.PrintReceipt(cashierName);

                _messageBoxService.ShowMessage("Оплата прошла успешно.");
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
            SelectedRun = new RunViewModel(order.SelectedRun);
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

            Passengers.Add(new PassengerViewModel() { });
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
                   string.IsNullOrWhiteSpace(passenger.Patronymic))
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
}
