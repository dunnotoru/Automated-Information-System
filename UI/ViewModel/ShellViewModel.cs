using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UI.Stores;

namespace UI.ViewModel
{
    internal class ShellViewModel : ViewModelBase
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
            }
        }

        public ObservableCollection<MenuItemViewModel> Items { get; set; }

        public ShellViewModel(NavigationStore navigationStore,
            List<MenuItemViewModel> menuItems)
        {
            Items = new ObservableCollection<MenuItemViewModel>(menuItems);
            
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        public void OnCurrentViewModelChanged()
        {
            NotifyPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
