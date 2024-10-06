﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using InformationSystem.Domain.Services;
using InformationSystem.Stores;

namespace InformationSystem.ViewModel;

internal class ShellViewModel : ViewModelBase, IDisposable
{
    private readonly ScheduleService _scheduleService;
    private NavigationStore _navigationStore;

    public ObservableCollection<MenuItemViewModel> Items { get; set; }

    public ViewModelBase CurrentViewModel
    {
        get => _navigationStore.CurrentViewModel;
        set
        {
            _navigationStore.CurrentViewModel = value;
            OnPropertyChangedByName(nameof(CurrentViewModel));
        }
    }

    public NavigationStore NavigationStore
    {
        get => _navigationStore;
        set
        {
            _navigationStore = value;
            OnPropertyChangedByName(nameof(NavigationStore));
        }
    }

    public ShellViewModel(NavigationStore navigationStore,
        List<MenuItemViewModel> menuItems,
        ScheduleService scheduleService)
    {
        Items = new ObservableCollection<MenuItemViewModel>();
        foreach (var item in menuItems)
        {
            item.ViewModelChanged += OnViewModelChanged;
            Items.Add(item);
        }

        _navigationStore = navigationStore;
        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

        _scheduleService = scheduleService;
    }

    private void OnViewModelChanged(object? sender, Func<ViewModelBase> getViewModel)
    {
        _navigationStore.CurrentViewModel = getViewModel();
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChangedByName(nameof(CurrentViewModel));
    }

    public void Dispose()
    {
        _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
    }
}