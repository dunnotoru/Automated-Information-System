using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UI.Command;
using UI.Stores;

namespace UI.ViewModel
{
    internal class MainMenuItemViewModel : ViewModelBase
    {
        private string _header;
        public string Header
        {
            get => _header;
            set { _header = value; OnPropertyChanged(); }
        }

        public bool isReadRequired { get; }
        public bool isWriteRequired { get; }
        public bool isDeleteRequired { get; }
        public bool isEditRequired { get; }

        public bool Visible { get => !isReadRequired; }

        public ObservableCollection<MainMenuItemViewModel> Items { get; set; }

        public Func<ViewModelBase> GetViewModel { get; set; }

        public MainMenuItemViewModel(IEnumerable<MainMenuItemViewModel> subItems)
        {
            ArgumentNullException.ThrowIfNull(subItems);
            Items = new ObservableCollection<MainMenuItemViewModel>(subItems);

            isReadRequired = false;
            isWriteRequired = false;
            isEditRequired = false;
            isDeleteRequired = false;
        }

        public MainMenuItemViewModel()
        {
            Items = new ObservableCollection<MainMenuItemViewModel>();

            isReadRequired = false;
            isWriteRequired = false;
            isEditRequired = false;
            isDeleteRequired = false;
        }

        private ParamRelayCommand<NavigationStore> _changeViewCommand;
        public ParamRelayCommand<NavigationStore> MenuItemCommand
        {
            get => _changeViewCommand ?? new ParamRelayCommand<NavigationStore>((obj) => Handler(obj));
        }

        private void Handler(NavigationStore parameter)
        {
            parameter.CurrentViewModel = GetViewModel();
        }
    }
}
