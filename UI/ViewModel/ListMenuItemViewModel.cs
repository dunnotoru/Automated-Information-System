using System;

namespace UI.ViewModel
{
    public class ListMenuItemViewModel : ViewModelBase
    {
        private string _name;
        private Func<ViewModelBase> _viewModel;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public Func<ViewModelBase> ViewModel
        {
            get => _viewModel;
            set { _viewModel = value; OnPropertyChanged(); }
        }

        public ViewModelBase ItemViewModel
        {
            get => _viewModel();
        }
    }
}
