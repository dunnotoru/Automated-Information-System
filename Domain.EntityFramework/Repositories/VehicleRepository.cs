using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        public int Create(Vehicle entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
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
            using (ApplicationContext context = new ApplicationContext())
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
            using (ApplicationContext context = new ApplicationContext())
            {
                entity.Id = id;
                context.Vehicles.Attach(entity);
                context.Vehicles.Update(entity);
                context.SaveChanges();
            }
        }

        public Vehicle GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
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
            using (ApplicationContext context = new ApplicationContext())
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
            using (ApplicationContext context = new ApplicationContext())
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
            using (ApplicationContext context = new ApplicationContext())
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
}
