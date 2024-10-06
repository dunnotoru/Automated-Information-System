using InformationSystem.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class VehicleModelEditViewModel : EditViewModel
{
    public VehicleModelEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        
    }

    protected override bool CanSave()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExecuteSave()
    {
        throw new System.NotImplementedException();
    }

    protected override void ExecuteRemove()
    {
        throw new System.NotImplementedException();
    }
}