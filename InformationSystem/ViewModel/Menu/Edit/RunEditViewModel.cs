using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class RunEditViewModel : EditViewModel
{
    public RunEditViewModel(Run run, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }
    
    public RunEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }

    public override IRelayCommand SaveCommand { get; }
    public override IRelayCommand RemoveCommand { get; }

    protected override bool CanSave()
    {
        throw new NotImplementedException();
    }
}