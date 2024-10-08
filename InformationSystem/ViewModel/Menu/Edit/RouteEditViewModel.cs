using System;
using System.Windows.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class RouteEditViewModel : EditViewModel
{
    private string _name = string.Empty;

    public RouteEditViewModel(Route route, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }
    
    public RouteEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }

    public override ICommand SaveCommand { get; }
    public override ICommand RemoveCommand { get; }

    protected override bool CanSave()
    {
        throw new NotImplementedException();
    }
}