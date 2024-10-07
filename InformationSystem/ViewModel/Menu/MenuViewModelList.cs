using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Services.Abstractions;
using InformationSystem.ViewModel.Factories;
using InformationSystem.ViewModel.Menu.Edit;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu;

public class BrandMenuViewModel : MenuViewModel<BrandEditViewModel, Brand>
{
    public BrandMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class CategoryMenuViewModel : MenuViewModel<CategoryEditViewModel, Category>
{
    public CategoryMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class DriverMenuViewModel : MenuViewModel<DriverEditViewModel, Driver>
{
    public DriverMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class FreighterMenuViewModel : MenuViewModel<FreighterEditViewModel, Freighter>
{
    public FreighterMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class RepairTypeMenuViewModel : MenuViewModel<RepairTypeEditViewModel, RepairType>
{
    public RepairTypeMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class RouteMenuViewModel : MenuViewModel<RouteEditViewModel, Route>
{
    public RouteMenuViewModel(IMessageBoxService messageBoxService,
        IDbContextFactory<DomainContext> contextFactory,
        IViewModelFactory vmFactory) : base(messageBoxService,
        contextFactory,
        vmFactory) { }
}

public class RunMenuViewModel : MenuViewModel<RunEditViewModel, Run>
{
    public RunMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
    }
}

public class VehicleMenuViewModel : MenuViewModel<VehicleEditViewModel, Vehicle>
{
    public VehicleMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
    }
}

public class VehicleModelMenuViewModel : MenuViewModel<VehicleModelEditViewModel, VehicleModel>
{
    public VehicleModelMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
    }
}

public class StationMenuViewModel : MenuViewModel<StationEditViewModel, Station>
{
    public StationMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
    }
}

public class TicketMenuViewModel : MenuViewModel<TicketEditViewModel, Ticket>
{
    public TicketMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
    }
}

public class TicketTypeMenuViewModel : MenuViewModel<TicketTypeEditViewModel, Ticket>
{
    public TicketTypeMenuViewModel(IMessageBoxService messageBoxService, IDbContextFactory<DomainContext> contextFactory, IViewModelFactory vmFactory) : base(messageBoxService, contextFactory, vmFactory)
    {
        
    }
}