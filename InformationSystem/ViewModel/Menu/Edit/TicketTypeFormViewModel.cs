using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class TicketTypeFormViewModel : EditViewModel
{
    private readonly TicketType _ticketType;
    
    protected override int? Save(DomainContext context)
    {
        context.TicketTypes.Update(_ticketType);
        context.SaveChanges();
        return _ticketType.Id;
    }

    protected override void Remove(DomainContext context)
    {
        context.TicketTypes.Remove(_ticketType);
        context.SaveChanges();
    }

    public TicketTypeFormViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _ticketType = new TicketType();
    }
    
    public TicketTypeFormViewModel(TicketType ticketType, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _ticketType = ticketType;
        Id = _ticketType.Id;
    }

    protected override bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(Name) && _ticketType.PriceModifierInPercent is > 0 and <= 100;
    }

    public string Name
    {
        get => _ticketType.Name;
        set { _ticketType.Name = value; RaisePropertyChanged(); }
    }

    public int PriceModifierInPercent
    {
        get => _ticketType.PriceModifierInPercent;
        set { _ticketType.PriceModifierInPercent = value; RaisePropertyChanged(); }
    }
}