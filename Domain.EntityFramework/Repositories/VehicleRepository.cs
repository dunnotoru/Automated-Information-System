using Domain.EntityFramework.Context;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly IDbContextFactory<DomainContext> _factory;

    public VehicleRepository(IDbContextFactory<DomainContext> factory)
    {
        _factory = factory;
    }

    public int Create(Vehicle entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (DomainContext context = _factory.CreateDbContext())
        {
            context.VehicleModels.Attach(entity.VehicleModel);
            context.Freighters.Attach(entity.Freighter);
            context.RepairTypes.Attach(entity.RepairType);
            context.Vehicles.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Remove(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            Vehicle stored = context.Vehicles.Include(o => o.Run).First(o => o.Id == id);
            if (stored.Run != null)
            {
                string message = stored.Run.Number + " ";
                throw new InvalidOperationException($"Этот транспорт участвует в рейсе: {message}");
            }
            context.Vehicles.Remove(stored);
            context.SaveChanges();
        }
    }

    public void Update(int id, Vehicle entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (DomainContext context = _factory.CreateDbContext())
        {
            entity.Id = id;
            context.Vehicles.Attach(entity);
            context.Vehicles.Update(entity);
            context.SaveChanges();
        }
    }

    public Vehicle GetById(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Vehicles
                .Include(o => o.Freighter)
                .Include(o => o.RepairType)
                .Include(o => o.VehicleModel).ThenInclude(x => x.Brand)
                .First(o => o.Id == id);
        }
    }

    public Vehicle GetByLicenseNumber(string licensePlateNumber)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Vehicles
                .Include(o => o.Freighter)
                .Include(o => o.RepairType)
                .Include(o => o.VehicleModel).ThenInclude(x => x.Brand)
                .First(o => o.LicensePlateNumber == licensePlateNumber);
        }
    }

    public IEnumerable<Vehicle> GetAll()
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Vehicles
                .Include(o => o.Freighter)
                .Include(o => o.RepairType)
                .Include(o => o.VehicleModel).ThenInclude(x => x.Brand)
                .ToList();
        }
    }

    public IEnumerable<Vehicle> GetIdleVehicles()
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            try
            {
                return context.Vehicles
                    .Include(o => o.Freighter)
                    .Include(o => o.RepairType)
                    .Include(o => o.VehicleModel).ThenInclude(x => x.Brand)
                    .Where(o => o.Run == null)
                    .ToList();
            }
            catch
            {
                return new List<Vehicle>();
            }
        }
    }
}