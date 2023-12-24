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
                context.Brands.Attach(entity.VehicleModel.Brand);
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
                Vehicle stored = context.Vehicles.First(o => o.Id == id);
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
    }
}
