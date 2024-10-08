using System.Windows.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class VehicleModelEditViewModel : EditViewModel
{

    public VehicleModelEditViewModel(VehicleModel vehicleModel, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }
    
    public VehicleModelEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }

    public override ICommand SaveCommand { get; }
    public override ICommand RemoveCommand { get; }

    protected override bool CanSave()
    {
        throw new System.NotImplementedException();
    }
}