using Domain.Models;
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
        private int _price;
        private int _cash;

        private DateTime _departureDateTime;
        private DateTime _arrivalDateTime;
        private Station _departureStation;
        private Station _arrivalStation;
        private Run _selectedRun;

        public DateTime DepartureDateTime
        {
            get => _departureDateTime;
            set
            {
                _departureDateTime = value;
                NotifyPropertyChanged(nameof(DepartureDateTime));
            }
        }
        public DateTime ArrivalDateTime
        {
            get => _arrivalDateTime;
            set
            {
                _arrivalDateTime = value;
                NotifyPropertyChanged(nameof(ArrivalDateTime));
            }
        }
        public Station DepartureStation
        {
            get => _departureStation;
            set
            {
                _departureStation = value;
                NotifyPropertyChanged(nameof(DepartureStation));
            }
        }
        public Station ArrivalStation
        {
            get => _arrivalStation;
            set
            {
                _arrivalStation = value;
                NotifyPropertyChanged(nameof(ArrivalStation));
            }
        }
        public Run SelectedRun
        {
            get => _selectedRun;
            set
            {
                _selectedRun = value;
                NotifyPropertyChanged(nameof(SelectedRun));
            }
        }

        private PassengerViewModel _selectedPassenger;

        public ObservableCollection<PassengerViewModel> Passengers { get; set; }

        public PassengerViewModel SelectedPassenger
        {
            get => _selectedPassenger;
            set { _selectedPassenger = value; NotifyPropertyChanged(nameof(SelectedPassenger)); }
        }

        public int Price
        {
            get => _price;
            set 
            { 
                _price = value; 
                NotifyPropertyChanged(nameof(Price)); 
                NotifyPropertyChanged(nameof(Change));
            }
        }

        public int Cash
        {
            get => _cash;
            set
            {
                _cash = value;
                NotifyPropertyChanged(nameof(Cash));
                NotifyPropertyChanged(nameof(Change));
            }
        }

        public int Change
        {
            get => Cash - Price;
        }

        public ICommand AddPassengerCommand
        {
            get => new RelayCommand(AddPassenger);
        }
        public ICommand DeletePassengerCommand
        {
            get => new RelayCommand(DeletePassenger);
        }

        public ICommand DeclineCommand
        {
            get => new RelayCommand(() =>
            {
                _navigationService.Navigate<RunSearchViewModel>();
            });
        }
        public ICommand SellCommand
        {
            get => new RelayCommand(SellTicket, CanSell);
        }


        private readonly OrderStore _orderStore;
        private readonly NavigationService _navigationService;
        
        public PassengerRegistrationViewModel(NavigationService navigationService, 
            OrderStore orderStore)
        {
            _orderStore = orderStore;
            _orderStore.OrderCreated += OnOrderCreated;
            Passengers = new ObservableCollection<PassengerViewModel>();
            _navigationService = navigationService;
        }

        private void SellTicket()
        {

        }

        private void OnOrderCreated(OrderViewModel order)
        {
            DepartureStation = order.DepartureStation;
            ArrivalStation = order.ArrivalStation;
            SelectedRun = order.SelectedRun;
        }

        private void AddPassenger()
        {
            if (Passengers.Count > 0) { 
                PassengerViewModel viewModel = Passengers.Last();

                if(ValidatePassenger(viewModel))
                    return;
            }

            Passengers.Add(new PassengerViewModel() { } );
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
            
            if (Price <= 0) return false;
            if(Cash <= 0) return false;
            if(Change < 0) return false;

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
