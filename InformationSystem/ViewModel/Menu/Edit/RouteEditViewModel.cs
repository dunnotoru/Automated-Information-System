using System;
using InformationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class RouteEditViewModel : EditViewModel
{
    private string _name;

    public RouteEditViewModel(IDbContextFactory<DomainContext> contextFactory, string name) : base(contextFactory)
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
}