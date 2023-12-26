using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Timers;
using UI.ViewModel.HelperViewModels;

namespace UI.ViewModel
{
    internal class ScheduleDataViewModel : ViewModelBase, IDisposable
    {
		private readonly IScheduleRepository _scheduleRepository;
        private static Timer _scheduleUpdateTimer;

        private ObservableCollection<ScheduleViewModel> _items;

        public ScheduleDataViewModel(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;

            Items = new ObservableCollection<ScheduleViewModel>();
            UpdateSchedule();

            _scheduleUpdateTimer = new Timer();
            _scheduleUpdateTimer.Interval = 1000 * 10;
            _scheduleUpdateTimer.Elapsed += OnTimerElapsed; ;
            _scheduleUpdateTimer.Start();
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateSchedule();
        }

        private void UpdateSchedule()
        {
            Items.Clear();
            foreach (var item in _scheduleRepository.GetAll())
            {
                ScheduleViewModel vm = new ScheduleViewModel(item);
                Items.Add(vm);
            }
        }
        
        public ObservableCollection<ScheduleViewModel> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(); }
        }
        public void Dispose()
        {
            _scheduleUpdateTimer.Stop();
            _scheduleUpdateTimer.Dispose();
        }
    }
}
