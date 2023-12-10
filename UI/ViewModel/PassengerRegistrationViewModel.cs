using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel
{
    public class PassengerRegistrationViewModel : ViewModelBase
    {
        private int _price;
        private int _cash;

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

        public NavigateCommand DeclineCommand { get; }
        public ICommand SellCommand
        {
            get => new RelayCommand(SellTicket, CanSell);
        }


        public PassengerRegistrationViewModel(NavigationService runSearchNavigationService)
        {
            Passengers = new ObservableCollection<PassengerViewModel>();
            DeclineCommand = new NavigateCommand(runSearchNavigationService);
        }

        private void SellTicket()
        {

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
    }
}
