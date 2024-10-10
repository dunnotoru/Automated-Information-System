using System;
using System.Windows.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class RouteEditViewModel : EditViewModel
{
    private readonly Route _route;

    public RouteEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _route = new Route();
    }
    
    public RouteEditViewModel(Route route, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _route = route;
    }

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
        throw new NotImplementedException();
    }
}