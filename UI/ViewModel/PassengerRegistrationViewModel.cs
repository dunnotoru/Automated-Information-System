using System;
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

        public PassengerRegistrationViewModel(NavigationService runSearchNavigationService, 
            NavigationService sellViewModel)
        {
            Passengers = new ObservableCollection<PassengerViewModel>();

            DeclineCommand = new NavigateCommand(runSearchNavigationService);
            SellCommand = new NavigateCommand(sellViewModel);
        }

        private void AddPassenger()
        {
            if (Passengers.Count > 0) { 
                PassengerViewModel viewModel = Passengers.Last();

                if(string.IsNullOrWhiteSpace(viewModel.Series) ||
                    string.IsNullOrWhiteSpace(viewModel.Number) ||
                    string.IsNullOrWhiteSpace(viewModel.Name) ||
                    string.IsNullOrWhiteSpace(viewModel.Surname) ||
                    string.IsNullOrWhiteSpace(viewModel.Patronymic)) {

                    return;
                }
            }

            Passengers.Add(new PassengerViewModel() { });
        }

        private void DeletePassenger()
        {
            if (SelectedPassenger == null)
                return;

            Passengers.Remove(SelectedPassenger);
        }
    }
}
