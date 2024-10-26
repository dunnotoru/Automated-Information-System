using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Services.Abstractions;
using InformationSystem.ViewModel.Factories;
using InformationSystem.ViewModel.Menu.Edit;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu;

public class BrandMenuViewModel : MenuViewModel<BrandFormViewModel, Brand>
{
    public BrandMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class CategoryMenuViewModel : MenuViewModel<CategoryFormViewModel, Category>
{
    public CategoryMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class DriverMenuViewModel : MenuViewModel<DriverFormViewModel, Driver>
{
    public DriverMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class FreighterMenuViewModel : MenuViewModel<FreighterFormViewModel, Freighter>
{
    public FreighterMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class RepairTypeMenuViewModel : MenuViewModel<RepairFormEditViewModel, RepairType>
{
    public RepairTypeMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class RouteMenuViewModel : MenuViewModel<RouteFormViewModel, Route>
{
    public RouteMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class RunMenuViewModel : MenuViewModel<RunFormViewModel, Run>
{
    public RunMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
    }
}

public class VehicleMenuViewModel : MenuViewModel<VehicleFormViewModel, Vehicle>
{
    public VehicleMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
    }
}

public class VehicleModelMenuViewModel : MenuViewModel<VehicleModelFormViewModel, VehicleModel>
{
    public VehicleModelMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
    }
}

public class StationMenuViewModel : MenuViewModel<StationFormViewModel, Station>
{
    public StationMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
    }
}

public class TicketMenuViewModel : MenuViewModel<TicketFormViewModel, Ticket>
{
    public TicketMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
    }
}

public class TicketTypeMenuViewModel : MenuViewModel<TicketTypeFormViewModel, Ticket>
{
    public TicketTypeMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
        
    }
}