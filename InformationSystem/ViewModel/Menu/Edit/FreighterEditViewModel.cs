using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class FreighterEditViewModel : EditViewModel
{
    private readonly Freighter _freighter;

    protected override int? Save(DomainContext context)
    {
        context.Freighters.Update(_freighter);
        context.SaveChanges();
        return _freighter.Id;
    }

    protected override void Remove(DomainContext context)
    {
        context.Freighters.Remove(_freighter);
        context.SaveChanges();
    }

    protected override bool CanSave() => !string.IsNullOrEmpty(Name);

    public FreighterEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _freighter = new Freighter();
    }

    public FreighterEditViewModel(Freighter freighter, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _freighter = freighter;
        Id = _freighter.Id;
    }
    
    public string Name
    {
        get => _freighter.Name;
        set { _freighter.Name = value; RaisePropertyChanged(); }
    }

}