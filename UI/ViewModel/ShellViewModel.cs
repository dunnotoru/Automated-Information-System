using System.Collections.ObjectModel;
using UI.Stores;

namespace UI.ViewModel
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel
        {
            get => _navigationStore.CurrentViewModel;
            set => _navigationStore.CurrentViewModel = value;
        }

        public ObservableCollection<MenuItemViewModel> Items { get; set; }

        public ShellViewModel(NavigationStore navigationStore, RunSearchViewModel runs)
        {
            Items = new ObservableCollection<MenuItemViewModel>();
            
            _navigationStore = navigationStore;

            Items.Add(new MenuItemViewModel() { Header = "Please"});

            _navigationStore.CurrentViewModel = runs;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public void OnCurrentViewModelChanged()
        {
            NotifyPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
