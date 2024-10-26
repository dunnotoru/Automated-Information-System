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
            var t when t == typeof(Category) => new CategoryFormViewModel(_factory),
            var t when t == typeof(Driver) => new DriverFormViewModel(_factory),
            var t when t == typeof(Freighter) => new FreighterFormViewModel(_factory),
            var t when t == typeof(RepairType) => new RepairFormEditViewModel(_factory),
            var t when t == typeof(Route) => new RouteFormViewModel(_factory),
            var t when t == typeof(Run) => new RunFormViewModel(_factory),
            var t when t == typeof(Station) => new StationFormViewModel(_factory),
            var t when t == typeof(Ticket) => new TicketFormViewModel(_factory),
            var t when t == typeof(TicketType) => new TicketTypeFormViewModel(_factory),
            var t when t == typeof(Vehicle) => new VehicleFormViewModel(_factory),
            var t when t == typeof(VehicleModel) => new VehicleModelFormViewModel(_factory),
            var t when t == typeof(Brand) => new BrandFormViewModel(_factory),
            _ => throw new ArgumentOutOfRangeException(nameof(TEntity), typeof(TEntity), null)
        };

    public EditViewModel CreateEditViewModel<TEntity>(TEntity entity)
        where TEntity : EntityBase => entity switch
        {
            Category obj => new CategoryFormViewModel(obj, _factory),
            Driver obj => new DriverFormViewModel(obj, _factory),
            Freighter obj => new FreighterFormViewModel(obj, _factory),
            RepairType obj => new RepairFormEditViewModel(obj, _factory),
            Route obj => new RouteFormViewModel(obj, _factory),
            Run obj => new RunFormViewModel(obj, _factory),
            Station obj => new StationFormViewModel(obj, _factory),
            Ticket obj => new TicketFormViewModel(obj, _factory),
            TicketType obj => new TicketTypeFormViewModel(obj, _factory),
            Vehicle obj => new VehicleFormViewModel(obj, _factory),
            VehicleModel obj => new VehicleModelFormViewModel(obj, _factory),
            Brand obj => new BrandFormViewModel(obj, _factory), 
            _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
        };
}