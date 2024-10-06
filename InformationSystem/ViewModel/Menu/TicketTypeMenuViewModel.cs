using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;
using InformationSystem.Services.Abstractions;
using InformationSystem.ViewModel.Books.EditViewModels;

namespace InformationSystem.ViewModel.Books;

internal class TicketTypeMenuViewModel : ViewModelBase
{
    private readonly ITicketTypeRepository _ticketTypeRepository;
    private readonly IMessageBoxService _messageBoxService;

    private ObservableCollection<TicketTypeEditViewModel> _items;
    private TicketTypeEditViewModel _selectedtem;

    public ObservableCollection<TicketTypeEditViewModel> Items
    {
        get { return _items; }
        set { _items = value; NotifyPropertyChanged(); }
    }

    public TicketTypeEditViewModel SelectedItem
    {
        get => _selectedtem;
        set { _selectedtem = value; NotifyPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
    }

    public bool IsRedactingEnabled => SelectedItem != null;

    public ICommand AddCommand { get; }

    public TicketTypeMenuViewModel(ITicketTypeRepository ticketTypeRepository,
        IMessageBoxService messageBoxService)
    {
        ArgumentNullException.ThrowIfNull(ticketTypeRepository);
        _ticketTypeRepository = ticketTypeRepository;
        _messageBoxService = messageBoxService;

        Items = new ObservableCollection<TicketTypeEditViewModel>();
        IEnumerable<TicketType> stations = _ticketTypeRepository.GetAll();
        foreach (TicketType item in stations)
        {
            TicketTypeEditViewModel vm = new TicketTypeEditViewModel(item, _ticketTypeRepository);
            vm.Save += OnSave;
            vm.Error += OnError;
            vm.Remove += OnRemove;
            Items.Add(vm);
        }

        AddCommand = new RelayCommand(Add);
    }

    private void OnRemove(object? sender, EventArgs e)
    {
        TicketTypeEditViewModel vm = (TicketTypeEditViewModel)sender;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;
        if (Items.Remove(vm))
        {
            _messageBoxService.ShowMessage("Данные успешно удалены.");
        }
    }

    private void OnError(object? sender, Exception e)
    {
        _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
    }

    private void OnSave(object? sender, EventArgs e)
    {
        TicketTypeEditViewModel vm = (TicketTypeEditViewModel)sender;
        vm.Save -= OnSave;
        vm.Error -= OnError;
        vm.Remove -= OnRemove;

        TicketType ticketType = _ticketTypeRepository.GetById(vm.Id);
        TicketTypeEditViewModel updatedVm = new TicketTypeEditViewModel(ticketType, _ticketTypeRepository);

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
        TicketTypeEditViewModel vm = new TicketTypeEditViewModel(_ticketTypeRepository);
        vm.Save += OnSave;
        vm.Error += OnError;
        vm.Remove += OnRemove;

        Items.Add(vm);
        SelectedItem = vm;
    }
}