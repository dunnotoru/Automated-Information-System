using System;
using System.Collections.ObjectModel;
using UI.Command;

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

        private RelayCommand _changeViewCommand;

        public MenuItemViewModel()
        {
            Items = new ObservableCollection<MenuItemViewModel>();
            
            isReadRequired = false;
            isWriteRequired = false;
            isEditRequired = false;
            isDeleteRequired = false;
        }

        public RelayCommand MenuItemCommand
        {
            get => _changeViewCommand ?? new RelayCommand(Handler);
        }

        private void Handler()
        {
            throw new NotImplementedException();
        }
    }
}
