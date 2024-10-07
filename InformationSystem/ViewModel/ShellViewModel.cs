using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using InformationSystem.Stores;

namespace InformationSystem.ViewModel;

internal class ShellViewModel : ViewModelBase
{
    private readonly NavigationStore _navigationStore;

    public ObservableCollection<MenuItemViewModel> Items { get; }

    public ViewModelBase CurrentViewModel
    {
        get => _navigationStore.CurrentViewModel;
        set => _navigationStore.CurrentViewModel = value;
    }

    public ShellViewModel(NavigationStore navigationStore, List<MenuItemViewModel> menuItems)
    {
        Items = new ObservableCollection<MenuItemViewModel>(menuItems.Select(itemViewModel =>
        {
            itemViewModel.ViewModelChanged += OnViewModelChanged;
            return itemViewModel;
        }));
        
        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    ~ShellViewModel()
    {
        _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
    }

    private void OnViewModelChanged(object? sender, Func<ViewModelBase> getViewModel)
    {
        _navigationStore.CurrentViewModel = getViewModel();
    }

    private void OnCurrentViewModelChanged()
    {
        NotifyPropertyChanged(nameof(CurrentViewModel));
    }
}