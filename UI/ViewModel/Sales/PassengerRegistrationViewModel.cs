using Domain.Models;
using Domain.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.Stores;

namespace UI.ViewModel
{
    internal class PassengerRegistrationViewModel : ViewModelBase, IDisposable
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IDocumentPrintService _documentPrintService;
        private readonly ITicketPriceCalculator _ticketPriceCalculator;
        private readonly AccountStore _accountStore;

        private readonly OrderStore _orderStore;
        private readonly NavigationService _navigationService;

        private int _price;
        private int _cash;

        private DateTime _departureDateTime;
        private DateTime _arrivalDateTime;
        private Station _departureStation;
        private Station _arrivalStation;
        private Run _selectedRun;
        private PassengerViewModel _selectedPassenger;

        public ICommand AddPassengerCommand { get; }
        public ICommand DeletePassengerCommand { get; }
        public ICommand DeclineCommand { get; }
        public ICommand CashPaymentCommand { get; }
        public ICommand NoncashPaymentCommand { get; }

        public PassengerRegistrationViewModel(NavigationService navigationService, OrderStore orderStore,
            IMessageBoxService messageBoxService, IDocumentPrintService documentPrintService,
            ITicketPriceCalculator ticketPriceCalculator, AccountStore accountStore)
        {
            _orderStore = orderStore;
            _orderStore.OrderCreated += OnOrderCreated;
            Passengers = new ObservableCollection<PassengerViewModel>();
            _navigationService = navigationService;
            _messageBoxService = messageBoxService;
            _documentPrintService = documentPrintService;
            _ticketPriceCalculator = ticketPriceCalculator;
            _accountStore = accountStore;

            CashPaymentCommand = new RelayCommand(CashPayment, () => CanSell() && ValidateCash());
            NoncashPaymentCommand = new RelayCommand(NoncashPayment, CanSell);
            AddPassengerCommand = new RelayCommand(AddPassenger);
            DeletePassengerCommand = new RelayCommand(DeletePassenger);
            DeclineCommand = new RelayCommand(Decline);
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
        public Station DepartureStation
        {
            get => _departureStation;
            set { _departureStation = value; OnPropertyChanged(); }
        }
        public Station ArrivalStation
        {
            get => _arrivalStation;
            set { _arrivalStation = value; OnPropertyChanged(); }
        }
        public Run SelectedRun
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

        private void Decline()
        {
            _navigationService.Navigate<RunSearchViewModel>();
        }
        private void CashPayment()
        {
            PrintReceipt();
            PrintTicket();

            _messageBoxService.ShowMessage("Оплата прошла успешно.");
            _navigationService.Navigate<RunSearchViewModel>();
        }

        private void NoncashPayment()
        {
            PrintReceipt();
            PrintTicket();

            _messageBoxService.ShowMessage("Оплата прошла успешно.");
            _navigationService.Navigate<RunSearchViewModel>();
        }

        private void PrintTicket()
        {
            foreach (var item in Passengers)
            {
                Ticket ticket = new Ticket()
                {
                    BookDate = DateTime.Now,
                    Cashier = _accountStore.CurrentAccount.Username,
                    Passport = Passengers[0].GetPassport(),
                    Price = 10,
                    Run = SelectedRun,
                };
                IDocument ticketPrint = new TicketPrint(ticket);
                _documentPrintService.PrintDocument(ticketPrint);
            }
        }

        private void PrintReceipt()
        {
            Receipt receipt = new Receipt(Guid.NewGuid().ToString(),
                "Я устал", "дома", DateTime.Now, _accountStore.CurrentAccount.Username);

            foreach (var item in Passengers)
            {
                int price = _ticketPriceCalculator.CalcPrice(SelectedRun, null);
                ReceiptLine line = new ReceiptLine("Билет", price, 1);
                receipt.ReceiptLines.Add(line);
            }

            IDocument ticketPrint = new ReceiptPrint(receipt);
            _documentPrintService.PrintDocument(ticketPrint);
        }

        private void OnOrderCreated(OrderViewModel order)
        {
            DepartureStation = order.DepartureStation;
            ArrivalStation = order.ArrivalStation;
            SelectedRun = order.SelectedRun;
            DepartureDateTime = SelectedRun.DepartureDateTime;
            ArrivalDateTime = SelectedRun.EstimatedArrivalDateTime;
        }

        private void AddPassenger()
        {
            if (Passengers.Count > 0)
            {
                PassengerViewModel viewModel = Passengers.Last();

                if (ValidatePassenger(viewModel))
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
    }
}
