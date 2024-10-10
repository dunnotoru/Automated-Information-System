using System;
using System.Windows.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class RunEditViewModel : EditViewModel
{
    public RunEditViewModel(Run run, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }
    
    public RunEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
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