using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Domain.Services.Abstractions;
using UI.Command;
using UI.Services;
using UI.Services.Abstractions;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel.Dispatcher;

internal class RunMenuViewModel : ViewModelBase
{
    private readonly IRunRepository _runRepository;
    private readonly IRouteRepository _routeRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMessageBoxService _messageBoxService;
    private readonly IArrivalTimeCalculator _arrivalTimeCalculator;

    private RunEditViewModel _selectedRun;

    public ObservableCollection<RunEditViewModel> Runs { get; set; }

    public RunEditViewModel SelectedRun
    {
        get => _selectedRun;
        set { _selectedRun = value; NotifyPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
    }

    public bool IsRedactingEnabled => SelectedRun != null;

    public ICommand AddCommand { get; }

    public RunMenuViewModel(IRunRepository runRepository,
        IRouteRepository routeRepository,
        IVehicleRepository vehicleRepository,
        IMessageBoxService messageBoxService,
        IDriverRepository driverRepository,
        IScheduleRepository scheduleRepository,
        IArrivalTimeCalculator arrivalTimeCalculator)
    {
        ArgumentNullException.ThrowIfNull(runRepository);
        ArgumentNullException.ThrowIfNull(routeRepository);
        ArgumentNullException.ThrowIfNull(messageBoxService);
        ArgumentNullException.ThrowIfNull(driverRepository);
        ArgumentNullException.ThrowIfNull(arrivalTimeCalculator);

        _runRepository = runRepository;
        _routeRepository = routeRepository;
        _vehicleRepository = vehicleRepository;
        _messageBoxService = messageBoxService;
        _driverRepository = driverRepository;
        _scheduleRepository = scheduleRepository;
        _arrivalTimeCalculator = arrivalTimeCalculator;

        Runs = new ObservableCollection<RunEditViewModel>();
        foreach (var item in _runRepository.GetAll())
        {
            RunEditViewModel vm = new RunEditViewModel(item, _runRepository,
                _routeRepository, _vehicleRepository, _driverRepository, _scheduleRepository, _arrivalTimeCalculator);
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
            _routeRepository, _vehicleRepository, _driverRepository, _scheduleRepository, _arrivalTimeCalculator);
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
            _scheduleRepository,
            _arrivalTimeCalculator);

        updatedVm.Remove += OnRemove;
        updatedVm.Save += OnSave;
        updatedVm.Error += OnError;

        int index = Runs.IndexOf(vm);
        Runs.Insert(index, updatedVm);
        Runs.Remove(vm);

        _messageBoxService.ShowMessage("Данные успешно сохранены");
    }
}