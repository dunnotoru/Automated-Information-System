using System;
using System.Collections.ObjectModel;
using Domain.Models;
using Domain.RepositoryInterfaces;
using UI.Services;
using UI.Services.Abstractions;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel.Dispatcher;

internal class TicketMenuViewModel : ViewModelBase
{
    private readonly IMessageBoxService _messageBoxService;
    private readonly ITicketRepository _ticketRepository;

    private ObservableCollection<TicketEditViewModel> _items;
    private TicketEditViewModel _selectedItem;


    public TicketMenuViewModel(IMessageBoxService messageBoxService, ITicketRepository ticketRepository)
    {
        ArgumentNullException.ThrowIfNull(messageBoxService);
        ArgumentNullException.ThrowIfNull(ticketRepository);

        _messageBoxService = messageBoxService;
        _ticketRepository = ticketRepository;

        Items = new ObservableCollection<TicketEditViewModel>();
        foreach (Ticket item in _ticketRepository.GetAll())
        {
            TicketEditViewModel vm = new TicketEditViewModel(item);
            vm.Error += OnError;
            Items.Add(vm);
        }
    }

    private void OnError(object? sender, Exception e)
    {
        _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
    }

    public bool IsRedactingEnabled => SelectedItem != null;

    public ObservableCollection<TicketEditViewModel> Items
    {
        get { return _items; }
        set { _items = value; OnPropertyChanged(); }
    }

    public TicketEditViewModel SelectedItem
    {
        get { return _selectedItem; }
        set { _selectedItem = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
    }
}