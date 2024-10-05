using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Domain.Models;
using Domain.RepositoryInterfaces;
using UI.Command;
using UI.Services;
using UI.Services.Abstractions;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel.Dispatcher;

internal class VehicleMenuViewModel : ViewModelBase
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMessageBoxService _messageBoxService;
    private readonly IVehicleModelRepository _vehicleModelRepository;
    private readonly IRepairTypeRepository _repairTypeRepository;
    private readonly IFreighterRepository _freighterRepository;

    private VehicleEditViewModel _selectedVehicle;

    public ObservableCollection<VehicleEditViewModel> Vehicles { get; set; }
    public VehicleEditViewModel SelectedVehicle
    {
        get => _selectedVehicle;
        set { _selectedVehicle = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
    }

    public bool IsRedactingEnabled => SelectedVehicle != null;

    public ICommand AddCommand { get; }

    public VehicleMenuViewModel(IVehicleRepository vehicleRepository, IMessageBoxService messageBoxService,
        IVehicleModelRepository vehicleModelRepository, IRepairTypeRepository repairTypeRepository, IFreighterRepository freighterRepository)
    {
        _vehicleRepository = vehicleRepository;
        _messageBoxService = messageBoxService;
        _vehicleModelRepository = vehicleModelRepository;
        _repairTypeRepository = repairTypeRepository;
        _freighterRepository = freighterRepository;

        Vehicles = new ObservableCollection<VehicleEditViewModel>();
        foreach (Vehicle item in _vehicleRepository.GetAll())
        {
            VehicleEditViewModel viewModel = new VehicleEditViewModel(item,
                _vehicleRepository,
                _vehicleModelRepository,
                _repairTypeRepository,
                _freighterRepository);
            viewModel.Remove += OnRemove;
            viewModel.Error += OnError;
            viewModel.Save += OnSave;
            Vehicles.Add(viewModel);
        }

        AddCommand = new RelayCommand(Add);
    }

    private void Add()
    {
        VehicleEditViewModel vm = new VehicleEditViewModel(_vehicleRepository,
            _vehicleModelRepository,
            _repairTypeRepository,
            _freighterRepository);
        vm.Remove += OnRemove;
        vm.Save += OnSave;
        vm.Error += OnError;
        Vehicles.Add(vm);
        SelectedVehicle = vm;
    }

    private void OnSave(object? sender, EventArgs e)
    {
        VehicleEditViewModel vm = (VehicleEditViewModel)sender;
        vm.Remove -= OnRemove;
        vm.Error -= OnError;
        vm.Save -= OnSave;

        Vehicle vehicle = _vehicleRepository.GetById(vm.Id);
        VehicleEditViewModel updatedVm = new VehicleEditViewModel(vehicle,
            _vehicleRepository, _vehicleModelRepository, _repairTypeRepository, _freighterRepository);

        vm.Remove += OnRemove;
        vm.Save += OnSave;
        vm.Error += OnError;

        int index = Vehicles.IndexOf(vm);
        Vehicles.Insert(index, updatedVm);
        Vehicles.Remove(vm);

        _messageBoxService.ShowMessage("Данные успешно сохранены");
    }

    private void OnRemove(object? sender, EventArgs e)
    {
        VehicleEditViewModel vm = (VehicleEditViewModel)sender;
        vm.Remove-= OnRemove;
        vm.Error -= OnError;
        vm.Save -= OnSave;
        if (Vehicles.Remove(vm))
        {
            _messageBoxService.ShowMessage("Данные успешно удалены.");
        }
    }

    private void OnError(object? sender, Exception e)
    {
        _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
    }
}