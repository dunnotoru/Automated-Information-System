using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        public ObservableCollection<ListMenuItemViewModel> ViewModels { get; set; }

        private ListMenuItemViewModel _selectedItem;
        public ListMenuItemViewModel SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public SettingsViewModel(IEnumerable<ListMenuItemViewModel> menuItems)
        {
            ViewModels = new ObservableCollection<ListMenuItemViewModel>(menuItems);
        }
    }
}
