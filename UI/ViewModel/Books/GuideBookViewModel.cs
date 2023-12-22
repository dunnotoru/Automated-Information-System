using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    internal class GuideBookViewModel : ViewModelBase
    {
        public ObservableCollection<ListMenuItemViewModel> ViewModels { get; set; }

        private ListMenuItemViewModel _selectedItem;
        public ListMenuItemViewModel SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(); }
        }

        public GuideBookViewModel(IEnumerable<ListMenuItemViewModel> menuItems)
        {
            ViewModels = new ObservableCollection<ListMenuItemViewModel>(menuItems);
        }
    }
}
