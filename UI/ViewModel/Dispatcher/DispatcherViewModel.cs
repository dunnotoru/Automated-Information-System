using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    public enum State
    {
        None = 0,
        Add,
        Edit
    }

    internal class DispatcherViewModel : ViewModelBase
    {
        public ObservableCollection<DispatcherMenuItem> ViewModels { get; set; }

        private DispatcherMenuItem _selectedItem;
        public DispatcherMenuItem SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        public DispatcherViewModel(IEnumerable<DispatcherMenuItem> menuItems)
        {
            ViewModels = new ObservableCollection<DispatcherMenuItem>(menuItems);
        }
    }
}