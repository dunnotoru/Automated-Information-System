using System;

namespace UI.ViewModel
{
    internal class DispatcherMenuItem : ViewModelBase
    {
        private string _name;
        private Func<ViewModelBase> _viewModel;
        
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public Func<ViewModelBase> ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                OnPropertyChanged(nameof(ItemViewModel));
            }
        }

        public ViewModelBase ItemViewModel
        {
            get => _viewModel();
        }
    }
}
