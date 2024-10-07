using System;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.Menu;
using InformationSystem.ViewModel.Menu.Edit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InformationSystem.ViewModel.Factories;

public class ViewModelFactory : IViewModelFactory
{
    private readonly IServiceProvider _provider;
    private readonly IDbContextFactory<DomainContext> _factory;

    public ViewModelFactory(IServiceProvider provider, IDbContextFactory<DomainContext> factory)
    {
        _provider = provider;
        _factory = factory;
    }

    public ViewModelBase CreateViewModel<TViewModel>()
        where TViewModel : ViewModelBase
    {
        return (ViewModelBase)_provider.GetRequiredService<TViewModel>();
    }
    
    public EditViewModel CreateEditViewModel<TEntity>() where TEntity : EntityBase => typeof(TEntity) switch
        {
            var t when t == typeof(Category) => new CategoryEditViewModel(_factory),
            var t when t == typeof(Driver) => new DriverEditViewModel(_factory),
            var t when t == typeof(Freighter) => new FreighterEditViewModel(_factory),
            var t when t == typeof(RepairType) => new RepairTypeEditViewModel(_factory),
            var t when t == typeof(Route) => new RouteEditViewModel(_factory),
            var t when t == typeof(Run) => new RunEditViewModel(_factory),
            var t when t == typeof(Station) => new StationEditViewModel(_factory),
            var t when t == typeof(Ticket) => new TicketEditViewModel(_factory),
            var t when t == typeof(TicketType) => new TicketTypeEditViewModel(_factory),
            var t when t == typeof(Vehicle) => new VehicleEditViewModel(_factory),
            var t when t == typeof(VehicleModel) => new VehicleModelEditViewModel(_factory),
            var t when t == typeof(Brand) => new BrandEditViewModel(_factory),
            _ => throw new ArgumentOutOfRangeException(nameof(TEntity), typeof(TEntity), null)
        };

    public EditViewModel CreateEditViewModel<TEntity>(TEntity entity)
        where TEntity : EntityBase => entity switch
        {
            Category obj => new CategoryEditViewModel(obj, _factory),
            Driver obj => new DriverEditViewModel(obj, _factory),
            Freighter obj => new FreighterEditViewModel(obj, _factory),
            RepairType obj => new RepairTypeEditViewModel(obj, _factory),
            Route obj => new RouteEditViewModel(obj, _factory),
            Run obj => new RunEditViewModel(obj, _factory),
            Station obj => new StationEditViewModel(obj, _factory),
            Ticket obj => new TicketEditViewModel(obj, _factory),
            TicketType obj => new TicketTypeEditViewModel(obj, _factory),
            Vehicle obj => new VehicleEditViewModel(obj, _factory),
            VehicleModel obj => new VehicleModelEditViewModel(obj, _factory),
            Brand obj => new BrandEditViewModel(obj, _factory), 
            _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
        };
}