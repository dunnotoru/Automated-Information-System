using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class FreighterFormViewModel : EditViewModel
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

    public FreighterFormViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _freighter = new Freighter();
    }

    public FreighterFormViewModel(Freighter freighter, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
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