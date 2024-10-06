using System;
using System.Collections.Generic;
using System.Linq;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.Data.Repositories;

public class VehicleModelRepository : IVehicleModelRepository
{
    private readonly IDbContextFactory<DomainContext> _factory;

    public VehicleModelRepository(IDbContextFactory<DomainContext> factory)
    {
        _factory = factory;
    }

    public int Create(VehicleModel entity)
    {
        int id = 0;
        using (DomainContext context = _factory.CreateDbContext())
        {
            VehicleModel createdEntity = new VehicleModel();
            Brand brand = context.Brands.First(o => o.Id == entity.Brand.Id);
            createdEntity.Name = entity.Name;
            createdEntity.Brand = brand;
            createdEntity.Capacity = entity.Capacity;

            context.VehicleModels.Add(createdEntity);
            context.SaveChanges();

            id = createdEntity.Id;
        }
        return id;
    }
    public void Update(int id, VehicleModel entity)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            VehicleModel updatedEntity = context.VehicleModels.First(o => o.Id == id);
            Brand brand = context.Brands.First(o => o.Id == entity.Brand.Id);
            updatedEntity.Name = entity.Name;
            updatedEntity.Brand = brand;
            updatedEntity.Capacity = entity.Capacity;

            context.SaveChanges();
        }
    }

    public IEnumerable<VehicleModel> GetAll()
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.VehicleModels.Include(o => o.Brand).ToList();
        }
    }

    public VehicleModel GetById(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.VehicleModels.Include(o => o.Brand).First(o => o.Id == id);
        }
    }

    public void Remove(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            VehicleModel stored = context.VehicleModels.Include(o => o.Vehicles).First(o => o.Id == id);
            if (stored.Vehicles.Count > 0)
            {
                string message = "";
                foreach (Vehicle vehicle in stored.Vehicles)
                {
                    message += vehicle.LicensePlateNumber + " ";
                }
                throw new InvalidOperationException($"В системе присутствуют транспортные средства этой модели: {message}");
            }

            context.VehicleModels.Remove(stored);

            context.SaveChanges();
        }
    }

}