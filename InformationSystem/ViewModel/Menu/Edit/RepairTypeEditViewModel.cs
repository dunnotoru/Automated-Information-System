using System;
using InformationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class RepairTypeEditViewModel : EditViewModel
{
    private string _name = string.Empty;

    public RepairTypeEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }
    
    public RepairTypeEditViewModel(IDbContextFactory<DomainContext> contextFactory, string name) : base(contextFactory)
    {
        _name = name;
    }
    
    protected override bool CanSave()
    {
        throw new NotImplementedException();
    }

    protected override void ExecuteSave()
    {
        throw new NotImplementedException();
    }

    protected override void ExecuteRemove()
    {
        throw new NotImplementedException();
    }

    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }
}