using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class RepairTypeEditViewModel : EditViewModel
{
    private string _name = string.Empty;
    
    public override ICommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new RepairType
        {
            Id = this.Id,
            Name = _name
        }), CanSave);
    
    public override ICommand RemoveCommand => new RelayCommand(ExecuteRemove<RepairType>);

    public RepairTypeEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }

    public RepairTypeEditViewModel(RepairType repairType, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = repairType.Id;
        _name = repairType.Name;
    }

    protected override bool CanSave()
    {
        return true;
    }

    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }
}