using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using UI.Command;
using UI.Stores;
using UI.View;

namespace UI.ViewModel
{
    public class MenuItemViewModel : ViewModelBase
    {
        private string _header;
        public string Header
        {
            get => _header;
            set
            {
                _header = value;
                NotifyPropertyChanged(nameof(Header));
            }
        }

        public bool isReadRequired { get; }
        public bool isWriteRequired { get; }
        public bool isDeleteRequired { get; }
        public bool isEditRequired { get; }

        public bool Visible { get => !isReadRequired; }

        public ObservableCollection<MenuItemViewModel> Items { get; set; }


        public MenuItemViewModel()
        {
            Items = new ObservableCollection<MenuItemViewModel>();
            
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
            parameter.CurrentViewModel = new TicketSaleViewModel();
        }
    }
}
