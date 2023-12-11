using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        public void Add(Vehicle entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Vehicles.Add(entity);
                context.SaveChanges();
            }
        }

        public void Remove(Vehicle entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                Vehicle? stored = context.Vehicles.SingleOrDefault(o => o.Id == entity.Id);
                if (stored == null) return;

                context.Vehicles.Remove(stored);
                context.SaveChanges();
            }
        }

        public void Update(Vehicle entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                Vehicle? stored = context.Vehicles.SingleOrDefault(o => o.Id == entity.Id);
                if (stored == null) return;
                stored = entity;

                context.SaveChanges();
            }
        }

        public Vehicle? GetByLicenseNumber(string licensePlateNumber)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Vehicles.SingleOrDefault(o => o.LicensePlateNumber == licensePlateNumber);
            }
        }

        public IEnumerable<Vehicle> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Vehicles.ToList();
            }
        }
    }
}
