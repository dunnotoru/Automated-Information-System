using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UI.Stores;

namespace UI.ViewModel
{
    internal class ShellViewModel : ViewModelBase, IDisposable
    {
        private NavigationStore _navigationStore;
        public ViewModelBase CurrentViewModel
        {
            get => _navigationStore.CurrentViewModel;
            set
            {
                _navigationStore.CurrentViewModel = value;
                OnPropertyChangedByName(nameof(CurrentViewModel));
            }
        }

        public NavigationStore NavigationStore
        {
            get => _navigationStore;
            set
            {
                _navigationStore = value;
                OnPropertyChangedByName(nameof(NavigationStore));
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
            OnPropertyChangedByName(nameof(CurrentViewModel));
        }

        public void Dispose()
        {
            _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
        }
    }
}
