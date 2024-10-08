using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class TicketEditViewModel : EditViewModel
{
    private int _price = 0;
    private DateTime _bookDate = DateTime.Now;
    private string _cashier = string.Empty;

    private int _runId = 0;
    private int _identityDocumentId = 0;
    private int _ticketTypeId = 0;
    
    public override IRelayCommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Ticket
        {
            Id = this.Id,
            BookDate = _bookDate,
            Cashier = _cashier,
            RunId = _runId,
            IdentityDocumentId = _identityDocumentId,
            TicketTypeId = _ticketTypeId
        }), CanSave);
    public override IRelayCommand RemoveCommand => new RelayCommand(ExecuteRemove<TicketType>);
    
    public TicketEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }
    public TicketEditViewModel(Ticket ticket, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _price = ticket.Price;
        _bookDate = ticket.BookDate;
        _cashier = ticket.Cashier;
        _runId = ticket.RunId;
        _identityDocumentId = ticket.IdentityDocumentId;
        _ticketTypeId = ticket.TicketTypeId;
    }

    protected override bool CanSave()
    {
        return true; // TODO: validate
    }
    
    public int Price
    {
        get => _price;
        set => SetProperty(ref _price, value);
    }

    public DateTime BookDate
    {
        get => _bookDate;
        set => SetProperty(ref _bookDate, value);
    }

    public string Cashier
    {
        get => _cashier;
        set => SetProperty(ref _cashier, value);
    }

    public int RunId
    {
        get => _runId;
        set => SetProperty(ref _runId, value);
    }

    public int IdentityDocumentId => _identityDocumentId;

    public int TicketTypeId => _ticketTypeId;
}