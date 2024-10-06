namespace InformationSystem.ViewModel.Factories;

internal interface IViewModelFactory
{
    ViewModelBase CreateViewModel<TViewModel>()
        where TViewModel : ViewModelBase;
}