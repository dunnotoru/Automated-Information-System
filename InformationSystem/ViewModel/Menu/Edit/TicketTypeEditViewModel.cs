using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class TicketTypeEditViewModel : EditViewModel
{
    private string _name = string.Empty;
    private int _priceModifierInPercent = 100;
    
    public override IRelayCommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new TicketType
        {
            Id = this.Id,
            Name = _name,
            PriceModifierInPercent = _priceModifierInPercent
        }), CanSave);
    public override IRelayCommand RemoveCommand => new RelayCommand(ExecuteRemove<TicketType>);
    
    public TicketTypeEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }
    public TicketTypeEditViewModel(TicketType ticketType, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = ticketType.Id;
        _name = ticketType.Name;
        _priceModifierInPercent = ticketType.PriceModifierInPercent;
    }

    protected override bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(Name) && _priceModifierInPercent is > 0 and <= 100;
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    public int PriceModifierInPercent
    {
        get => _priceModifierInPercent;
        set => SetProperty(ref _priceModifierInPercent, value);
    }
}