using Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Timers;
using UI.Stores;

namespace UI.ViewModel
{
    internal class ShellViewModel : ViewModelBase, IDisposable
    {
        private readonly ScheduleService _scheduleService;
        private NavigationStore _navigationStore;

        private static Timer _scheduleUpdateTimer;

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

        public ObservableCollection<MainMenuItemViewModel> Items { get; set; }

        public ShellViewModel(NavigationStore navigationStore,
            List<MainMenuItemViewModel> menuItems,
            ScheduleService scheduleService)
        {
            Items = new ObservableCollection<MainMenuItemViewModel>(menuItems);

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            _scheduleService = scheduleService;

            _scheduleUpdateTimer = new Timer();
            _scheduleUpdateTimer.Interval = 1000 * 10;
            _scheduleUpdateTimer.Elapsed += OnTimerElapsed;
            _scheduleUpdateTimer.Start();
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
}
