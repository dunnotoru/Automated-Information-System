using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Domain.Models;
using Domain.RepositoryInterfaces;
using UI.Command;
using UI.Services;
using UI.Services.Abstractions;
using UI.ViewModel.Books.EditViewModels;

namespace UI.ViewModel.Books;

internal class VehicleModelMenuViewModel : ViewModelBase
{
    private readonly IMessageBoxService _messageBoxService;
    private readonly IVehicleModelRepository _vehicleModelRepository;
    private readonly IBrandRepository _brandRepository;

    private ObservableCollection<VehicleModelEditViewModel> _items;
    private VehicleModelEditViewModel _selectedItem;

    public ICommand AddCommand { get; }

    public VehicleModelMenuViewModel(IMessageBoxService messageBoxService, IBrandRepository brandRepository, IVehicleModelRepository vehicleModelRepository)
    {
        ArgumentNullException.ThrowIfNull(messageBoxService);
        ArgumentNullException.ThrowIfNull(brandRepository);
        ArgumentNullException.ThrowIfNull(vehicleModelRepository);

        _messageBoxService = messageBoxService;
        _brandRepository = brandRepository;
        _vehicleModelRepository = vehicleModelRepository;

        Items = new ObservableCollection<VehicleModelEditViewModel>();
        foreach (VehicleModel item in _vehicleModelRepository.GetAll())
        {
            VehicleModelEditViewModel vm = new VehicleModelEditViewModel(item, _brandRepository, _vehicleModelRepository);
            vm.Save += OnSave;
            vm.Error += OnError;
            vm.Remove += OnRemove;
            Items.Add(vm);
        }

        AddCommand = new RelayCommand(Add);
    }

    private void OnRemove(object? sender, EventArgs e)
    {
        VehicleModelEditViewModel vm = (VehicleModelEditViewModel)sender;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;
        Items.Remove(vm);

        _messageBoxService.ShowMessage("Данные успешно удалены.");
    }

    private void OnSave(object? sender, EventArgs e)
    {
        VehicleModelEditViewModel vm = (VehicleModelEditViewModel)sender;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;

        VehicleModel vehicleModel = _vehicleModelRepository.GetById(vm.Id);
        VehicleModelEditViewModel updatedVm = new VehicleModelEditViewModel(vehicleModel, _brandRepository, _vehicleModelRepository);

        updatedVm.Remove += OnRemove;
        updatedVm.Save += OnSave;
        updatedVm.Error += OnError;

        int index = Items.IndexOf(vm);
        Items.Insert(index, updatedVm);
        Items.Remove(vm);

        _messageBoxService.ShowMessage("Данные успешно сохранены.");
    }

    private void OnError(object? sender, Exception e)
    {
        _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
    }

    private void Add()
    {
        VehicleModelEditViewModel vm = new VehicleModelEditViewModel(_brandRepository, _vehicleModelRepository);
        vm.Save += OnSave;
        vm.Error += OnError;
        vm.Remove += OnRemove;
        Items.Add(vm);
        SelectedItem = vm;
    }

    public bool IsRedactingEnabled => SelectedItem != null;

    public ObservableCollection<VehicleModelEditViewModel> Items
    {
        get { return _items; }
        set { _items = value; OnPropertyChanged(); }
    }

    public VehicleModelEditViewModel SelectedItem
    {
        get { return _selectedItem; }
        set { _selectedItem = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
    }
}