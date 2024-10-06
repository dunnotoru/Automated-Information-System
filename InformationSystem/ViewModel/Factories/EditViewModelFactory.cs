using System;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.Menu;
using InformationSystem.ViewModel.Menu.Edit;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Factories;

public class EditViewModelFactory
{
    private readonly IDbContextFactory<DomainContext> _factory;

    public EditViewModelFactory(IDbContextFactory<DomainContext> factory)
    {
        _factory = factory;
    }

    public EditViewModel CreateEditViewModel<TEntity>()
    {
        return typeof(TEntity) switch
        {
            //Account
            //Category
            //Driver
            //DriverLicense
            //Freighter
            //IdentityDocument
            //LicenseCategory
            //RepairType
            //Route
            //Run
            //Schedule
            //Station
            //StationRoute
            //Ticket
            //TicketType
            //Vehicle
            //VehicleModel
            var t when t == typeof(TEntity) => new BrandEditViewModel(_factory),
            _ => throw new ArgumentOutOfRangeException(nameof(TEntity), typeof(TEntity), null)
        };
    }
    
    public EditViewModel CreateEditViewModel<TEntity>(TEntity entity)
        where TEntity : EntityBase
    {
        return entity switch
        {
            //Account
            //Category
            //Driver
            //DriverLicense
            //Freighter
            //IdentityDocument
            //LicenseCategory
            //RepairType
            //Route
            //Run
            //Schedule
            //Station
            //StationRoute
            //Ticket
            //TicketType
            //Vehicle
            //VehicleModel
            Brand _ => new BrandEditViewModel(entity as Brand, _factory), 
            _ => throw new ArgumentOutOfRangeException(nameof(entity), entity, null)
        };
    }
}