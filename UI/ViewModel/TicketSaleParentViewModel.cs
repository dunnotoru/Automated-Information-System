using UI.Command;
using UI.Services;
using UI.Stores;

namespace UI.ViewModel
{
    class TicketSaleParentViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        
        public ViewModelBase CurrentViewModel
        {
            get => _navigationStore.CurrentViewModel;
            set
            {
                _navigationStore.CurrentViewModel = value;
                NotifyPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public TicketSaleParentViewModel(NavigationStore navigationStore,
            NavigationService runSearchViewModelNavigationService,
            NavigationService PassengerRegistrationNavigationService)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            runSearchViewModelNavigationService.Navigate();
        }

        public void OnCurrentViewModelChanged()
        {
            NotifyPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
