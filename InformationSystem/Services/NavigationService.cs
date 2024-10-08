using CommunityToolkit.Mvvm.ComponentModel;
using InformationSystem.Stores;
using InformationSystem.ViewModel.Factories;

namespace InformationSystem.Services;

internal class NavigationService
{
    private readonly NavigationStore _navigationStore;
    private readonly IViewModelFactory _viewModelFactory;

    public NavigationService(NavigationStore navigationStore, IViewModelFactory viewModelFactory)
    {
        _navigationStore = navigationStore;
        _viewModelFactory = viewModelFactory;
    }

    public void Navigate<TViewModel>() where TViewModel : ObservableObject
    {
        _navigationStore.CurrentViewModel = _viewModelFactory.CreateViewModel<TViewModel>();
    }
}