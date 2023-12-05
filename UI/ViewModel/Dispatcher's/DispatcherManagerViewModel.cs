using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    public class DispatcherManagerViewModel : ViewModelBase
    {
        private DispatcherMenuItem _selectedItem;
        public ObservableCollection<DispatcherMenuItem> ViewModels { get; set; }
        
        public DispatcherMenuItem SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; NotifyPropertyChanged(nameof(SelectedItem)); }
        }

        public DispatcherManagerViewModel(IEnumerable<DispatcherMenuItem> menuItems)
        {
            ViewModels = new ObservableCollection<DispatcherMenuItem>(menuItems);
        }
    }
}