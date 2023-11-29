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

        public ICommand AddPassengerCommand { get; }
        public ICommand EditPassengerCommand { get; }
        public ICommand DeletePassengerCommand { get; }


        public NavigateCommand DeclineCommand { get; }
        public NavigateCommand SellCommand { get; }

        public TicketSaleViewModel()
        {
            Passengers = new ObservableCollection<PassengerViewModel>()
            {
                new PassengerViewModel()
                {
                    Name = "имечко",
                    Surname = "фамилия",
                    Patronymic = "отчествович",
                    Series = "123",
                    Number = "123243"
                },
                new PassengerViewModel()
                {
                    Name = "четкий",
                    Surname = "потсан",
                    Patronymic = "крижевич",
                    Series = "1233",
                    Number = "1231"
                },
            };
        }
    }
}
