using System;
using InformationSystem.Stores;
using InformationSystem.ViewModel;
using InformationSystem.ViewModel.Factories;

namespace InformationSystem.Services;

internal class NavigationService
{
    private readonly NavigationStore _navigationStore;
    private readonly IViewModelFactory _viewModelFactory;

    public event EventHandler? CanExecuteChanged;

    public NavigationService(NavigationStore navigationStore, IViewModelFactory viewModelFactory)
    {
        _navigationStore = navigationStore;
        _viewModelFactory = viewModelFactory;
    }

    public void Navigate<TViewModel>() where TViewModel : ViewModelBase
    {
        _navigationStore.CurrentViewModel = _viewModelFactory.CreateViewModel<TViewModel>();
    }
}