using Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using UI.Stores;

namespace UI.ViewModel;

internal class ShellViewModel : ViewModelBase, IDisposable
{
    private readonly ScheduleService _scheduleService;
    private NavigationStore _navigationStore;

    private static Timer _scheduleUpdateTimer;
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

        _scheduleUpdateTimer = new Timer();
        _scheduleUpdateTimer.Interval = 1000* 5;
        _scheduleUpdateTimer.Elapsed += OnTimerElapsed;
        _scheduleUpdateTimer.Start();

    }

    private void OnViewModelChanged(object? sender, Func<ViewModelBase> getViewModel)
    {
        _navigationStore.CurrentViewModel = getViewModel();
    }

    private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        _scheduleService.UpdateSchedule();
    }

    public void OnCurrentViewModelChanged()
    {
        OnPropertyChangedByName(nameof(CurrentViewModel));
    }

    public void Dispose()
    {
        _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
    }
}