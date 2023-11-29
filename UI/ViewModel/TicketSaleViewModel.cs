using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public class TicketSaleViewModel : ViewModelBase
    {
        private string _departureStation;
        private string _arrivalStation;
        private DateTime _departureDateTime;
        private DateTime _arrivalDateTime;
        private int _price;
        
        public ObservableCollection<PassengerViewModel> Passengers {  get; set; }
        public PassengerViewModel SelectedPassenger {  get; set; }

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

        public ICommand AddPassengerCommand { get; }
        public ICommand EditPassengerCommand { get; }
        public ICommand DeletePassengerCommand { get; }



        public NavigateCommand DeclineCommand { get; }
        public NavigateCommand SellCommand { get; }

        public TicketSaleViewModel()
        {
            Passengers = new ObservableCollection<PassengerViewModel>();
            Passengers.Add(new PassengerViewModel());
        }
    }
}
