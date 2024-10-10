using System;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class TicketEditViewModel : EditViewModel
{
    private readonly Ticket _ticket;
    
    protected override int? Save(DomainContext context)
    {
        throw new NotImplementedException();
    }

    protected override void Remove(DomainContext context)
    {
        throw new NotImplementedException();
    }
    
    protected override bool CanSave()
    {
        return true; // TODO: validate
    }

    public TicketEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _ticket = new Ticket();
    }
    public TicketEditViewModel(Ticket ticket, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _ticket = ticket;
        Id = _ticket.Id;
    }

    public int Price
    {
        get => _ticket.Price;
        set { _ticket.Price = value; RaisePropertyChanged();}
    }

    public DateTime BookDate
    {
        get => _ticket.BookDate;
        set { _ticket.BookDate = value; RaisePropertyChanged();}
    }

    public string Cashier
    {
        get => _ticket.Cashier;
        set { _ticket.Cashier = value; RaisePropertyChanged();}
    }
}