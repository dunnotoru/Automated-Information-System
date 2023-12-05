namespace UI.ViewModel
{
    public class DispatcherMenuItem : ViewModelBase
    {
        private string _name;
        private ViewModelBase _viewModel;
        
        public string Name
        {
            get => _name;
            set { _name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public ViewModelBase ItemViewModel
        {
            get => _viewModel;
            set { _viewModel = value; NotifyPropertyChanged(nameof(ItemViewModel)); }
        }
    }
}
