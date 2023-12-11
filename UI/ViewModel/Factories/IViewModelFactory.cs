using Castle.Windsor;

namespace UI.ViewModel.Factories
{
    internal interface IViewModelFactory
    {
        ViewModelBase CreateViewModel<TViewModel>() 
            where TViewModel : ViewModelBase;
    }
}
