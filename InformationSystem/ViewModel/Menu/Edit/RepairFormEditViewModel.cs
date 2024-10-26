using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class RepairFormEditViewModel : EditViewModel
{
    private readonly RepairType _repairType;

    protected override int? Save(DomainContext context)
    {
        context.RepairTypes.Update(_repairType);
        context.SaveChanges();
        return _repairType.Id;
    }

    protected override void Remove(DomainContext context)
    {
        context.RepairTypes.Remove(_repairType);
        context.SaveChanges();
    }
    
    public RepairFormEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _repairType = new RepairType();
    }

    public RepairFormEditViewModel(RepairType repairType, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _repairType = repairType;
        Id = _repairType.Id;
    }

    protected override bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(Name);
    }

    public string Name
    {
        get => _repairType.Name;
        set { _repairType.Name = value; RaisePropertyChanged(); }
    }
}