using System.Collections.ObjectModel;
using UI.Stores;

namespace UI.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        private NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel
        {
            get => _navigationStore.CurrentViewModel;
            set  
            {
                _navigationStore.CurrentViewModel = value; 
                NotifyPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public NavigationStore NavigationStore
        {
            get => _navigationStore;
            set
            {
                _navigationStore = value;
                NotifyPropertyChanged(nameof(NavigationStore));
                NotifyPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ObservableCollection<MenuItemViewModel> Items { get; set; }

        public ShellViewModel(NavigationStore navigationStore, RunSearchViewModel runs)
        {
            Items = new ObservableCollection<MenuItemViewModel>();
            Items.Add(new MenuItemViewModel() { Header = "Please"});

            _navigationStore = navigationStore;
            CurrentViewModel = runs;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

        }

        public void OnCurrentViewModelChanged()
        {
            NotifyPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
