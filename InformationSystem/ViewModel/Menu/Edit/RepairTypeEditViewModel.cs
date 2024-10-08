using System;
using System.Windows.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class RepairTypeEditViewModel : EditViewModel
{
    private string _name = string.Empty;

    public RepairTypeEditViewModel(RepairType repairType, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }
    
    public RepairTypeEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }

    public override ICommand SaveCommand { get; }
    public override ICommand RemoveCommand { get; }

    protected override bool CanSave()
    {
        throw new NotImplementedException();
    }

    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }
}