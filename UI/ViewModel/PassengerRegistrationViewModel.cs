using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel
{
    public class PassengerRegistrationViewModel : ViewModelBase
    {
        private string _departureStation;
        private string _arrivalStation;
        private DateTime _departureDateTime;
        private DateTime _arrivalDateTime;
        private int _price;
        private PassengerViewModel _selectedPassenger;
        
        public ObservableCollection<PassengerViewModel> Passengers {  get; set; }
        
        public PassengerViewModel SelectedPassenger
        {
            get => _selectedPassenger;
            set
            {
                _selectedPassenger = value;
                NotifyPropertyChanged(nameof(SelectedPassenger));
            }
        }

        public string DepartureStation
        {
            get => _departureStation;
            set
            {
                _departureStation = value;
                NotifyPropertyChanged(nameof(DepartureStation));
            }
        }
        public string ArrivalStation
        {
            get => _arrivalStation;
            set
            {
                _arrivalStation = value;
                NotifyPropertyChanged(nameof(ArrivalStation));
            }
        }
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
        public int Price
        {
            get => _price;
            set
            {
                _price = value;
                NotifyPropertyChanged(nameof(Price));
            }
        }

        public ICommand AddPassengerCommand
        {
            get => new RelayCommand(AddPassenger);
        }
        public ICommand DeletePassengerCommand
        {
            get => new RelayCommand(DeletePassenger);
        }

        public NavigateCommand DeclineCommand { get; }
        public NavigateCommand SellCommand { get; }

        public PassengerRegistrationViewModel(NavigationService runSearchNavigationService, NavigationService sell)
        {
            Passengers = new ObservableCollection<PassengerViewModel>();

            DeclineCommand = new NavigateCommand(runSearchNavigationService);
            SellCommand = new NavigateCommand(sell);
        }

        private void AddPassenger()
        {
            Passengers.Add(new PassengerViewModel() { Name = "Имя" });
        }

        private void DeletePassenger()
        {
            if (SelectedPassenger == null)
                return;

            Passengers.Remove(SelectedPassenger);
        }
    }
}
