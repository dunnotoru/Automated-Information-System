using Domain.EntityFramework.Repositories;
using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel
{
    internal class RunMenuViewModel : ViewModelBase
    {
        private readonly IRunRepository _runRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMessageBoxService _messageBoxService;


        private RunEditViewModel _selectedRun;

        public ObservableCollection<RunEditViewModel> Runs { get; set; }

        public RunEditViewModel SelectedRun
        {
            get => _selectedRun;
            set { _selectedRun = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
        }

        public bool IsRedactingEnabled => SelectedRun != null;

        public ICommand AddCommand { get; }

        public RunMenuViewModel(IRunRepository runRepository, IRouteRepository routeRepository,
            IVehicleRepository vehicleRepository, IMessageBoxService messageBoxService, IDriverRepository driverRepository, IScheduleRepository scheduleRepository)
        {
            ArgumentNullException.ThrowIfNull(runRepository);
            ArgumentNullException.ThrowIfNull(routeRepository);
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(driverRepository);

            _runRepository = runRepository;
            _routeRepository = routeRepository;
            _vehicleRepository = vehicleRepository;
            _messageBoxService = messageBoxService;
            _driverRepository = driverRepository;
            _scheduleRepository = scheduleRepository;

            Runs = new ObservableCollection<RunEditViewModel>();
            foreach (var item in _runRepository.GetAll())
            {
                RunEditViewModel vm = new RunEditViewModel(item, _runRepository,
                    _routeRepository, _vehicleRepository, _driverRepository, _scheduleRepository);
                vm.Save += OnSave;
                vm.Remove += OnRemove;
                vm.Error += OnError;
                Runs.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void Add()
        {
            RunEditViewModel vm = new RunEditViewModel(_runRepository,
                    _routeRepository, _vehicleRepository, _driverRepository, _scheduleRepository);
            vm.Save += OnSave;
            vm.Remove += OnRemove;
            vm.Error += OnError;

            Runs.Add(vm);
            SelectedRun = vm;
        }

        private void OnError(object? sender, Exception e)
        {
            _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
        }

        private void OnRemove(object? sender, EventArgs e)
        {
            RunEditViewModel vm = (RunEditViewModel)sender;
            vm.Save -= OnSave;
            vm.Remove -= OnRemove;
            vm.Error -= OnError;
            if (Runs.Remove(vm))
            {
                _messageBoxService.ShowMessage("Данные успешно удалены.");
            }
        }

        private void OnSave(object? sender, EventArgs e)
        {
            RunEditViewModel vm = (RunEditViewModel)sender;
            vm.Save -= OnSave;
            vm.Remove -= OnRemove;
            vm.Error -= OnError;

            Run run = _runRepository.GetById(vm.Id);
            RunEditViewModel updatedVm = new RunEditViewModel(run,
                                                              _runRepository,
                                                              _routeRepository,
                                                              _vehicleRepository,
                                                              _driverRepository,
                                                              _scheduleRepository);

            updatedVm.Remove += OnRemove;
            updatedVm.Save += OnSave;
            updatedVm.Error += OnError;

            int index = Runs.IndexOf(vm);
            Runs.Insert(index, updatedVm);
            Runs.Remove(vm);

            _messageBoxService.ShowMessage("Данные успешно сохранены");
        }
    }
}
