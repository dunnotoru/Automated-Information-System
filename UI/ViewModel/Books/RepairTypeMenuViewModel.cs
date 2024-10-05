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

internal class RepairTypeMenuViewModel : ViewModelBase
{
    private readonly IMessageBoxService _messageBoxService;
    private readonly IRepairTypeRepository _repairTypeRepository;

    private ObservableCollection<RepairTypeEditViewModel> _items;
    private RepairTypeEditViewModel _selectedItem;

    public ICommand AddCommand { get; }

    public RepairTypeMenuViewModel(IMessageBoxService messageBoxService, IRepairTypeRepository repairType)
    {
        ArgumentNullException.ThrowIfNull(messageBoxService);
        ArgumentNullException.ThrowIfNull(repairType);

        _messageBoxService = messageBoxService;
        _repairTypeRepository = repairType;

        Items = new ObservableCollection<RepairTypeEditViewModel>();
        foreach (RepairType item in _repairTypeRepository.GetAll())
        {
            RepairTypeEditViewModel vm = new RepairTypeEditViewModel(item, _repairTypeRepository);
            vm.Save += OnSave;
            vm.Error += OnError;
            vm.Remove += OnRemove;
            Items.Add(vm);
        }

        AddCommand = new RelayCommand(Add);
    }

    private void OnRemove(object? sender, EventArgs e)
    {
        RepairTypeEditViewModel vm = (RepairTypeEditViewModel)sender;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;
        Items.Remove(vm);

        _messageBoxService.ShowMessage("Данные успешно удалены.");
    }

    private void OnError(object? sender, Exception e)
    {
        _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
    }

    private void OnSave(object? sender, EventArgs e)
    {
        RepairTypeEditViewModel vm = (RepairTypeEditViewModel)sender;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;

        RepairType brand = _repairTypeRepository.GetById(vm.Id);
        RepairTypeEditViewModel updatedVm = new RepairTypeEditViewModel(brand, _repairTypeRepository);

        updatedVm.Remove += OnRemove;
        updatedVm.Save += OnSave;
        updatedVm.Error += OnError;

        int index = Items.IndexOf(vm);
        Items.Insert(index, updatedVm);
        Items.Remove(vm);

        _messageBoxService.ShowMessage("Данные успешно сохранены.");
    }

    private void Add()
    {
        RepairTypeEditViewModel vm = new RepairTypeEditViewModel(_repairTypeRepository);
        vm.Save += OnSave;
        vm.Error += OnError;
        vm.Remove += OnRemove;
        Items.Add(vm);
        SelectedItem = vm;
    }

    public bool IsRedactingEnabled => SelectedItem != null;
    public ObservableCollection<RepairTypeEditViewModel> Items
    {
        get { return _items; }
        set { _items = value; NotifyPropertyChanged(); }
    }
    public RepairTypeEditViewModel SelectedItem
    {
        get { return _selectedItem; }
        set { _selectedItem = value; NotifyPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
    }
}