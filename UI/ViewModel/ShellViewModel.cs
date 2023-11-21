using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    public class ShellViewModel :ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set 
            {
                _currentViewModel = value; 
                NotifyPropertyChanged(nameof(CurrentViewModel));
            }
        }

        public ObservableCollection<MenuItemViewModel> Items { get; set; }

        public ShellViewModel()
        {
            CurrentViewModel = new CasshierViewModel();
            Items = new ObservableCollection<MenuItemViewModel>();
            Items.Add(new MenuItemViewModel() { Header = "Nigger" });
        }
    }
}
