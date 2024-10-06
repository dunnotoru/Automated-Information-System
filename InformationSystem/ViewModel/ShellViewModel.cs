using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using InformationSystem.Services;
using InformationSystem.Stores;

namespace InformationSystem.ViewModel;

internal class ShellViewModel : ViewModelBase, IDisposable
{
    private NavigationStore _navigationStore;

    public ObservableCollection<MenuItemViewModel> Items { get; private set; }

    public ViewModelBase CurrentViewModel
    {
        get => _navigationStore.CurrentViewModel;
        set
        {
            _navigationStore.CurrentViewModel = value;
            NotifyPropertyChanged(nameof(CurrentViewModel));
        }
    }

    public NavigationStore NavigationStore
    {
        get => _navigationStore;
        set
        {
            _navigationStore = value;
            NotifyPropertyChanged(nameof(NavigationStore));
        }
    }

    public ShellViewModel(NavigationStore navigationStore,
        List<MenuItemViewModel> menuItems)
    {
        Items = new ObservableCollection<MenuItemViewModel>();
        foreach (var item in menuItems)
        {
            item.ViewModelChanged += OnViewModelChanged;
            Items.Add(item);
        }

        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnViewModelChanged(object? sender, Func<ViewModelBase> getViewModel)
    {
        _navigationStore.CurrentViewModel = getViewModel();
    }

    private void OnCurrentViewModelChanged()
    {
        NotifyPropertyChanged(nameof(CurrentViewModel));
    }

    public void Dispose()
    {
        _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
    }
}