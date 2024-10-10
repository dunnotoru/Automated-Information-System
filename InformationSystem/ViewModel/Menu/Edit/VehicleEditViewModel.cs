using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class VehicleEditViewModel : EditViewModel
{
    public override ICommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Vehicle
        {
            Id = this.Id,
        }), CanSave);
    
    public override ICommand RemoveCommand => new RelayCommand(ExecuteRemove<Vehicle>);

    
    public VehicleEditViewModel(Vehicle vehicle, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }
    
    public VehicleEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }

    protected override bool CanSave()
    {
        return true;
    }

}