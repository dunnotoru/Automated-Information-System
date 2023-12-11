using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        public void Add(Driver entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Drivers.Add(entity);
                context.SaveChanges();
            }
        }

        public void Remove(Driver entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                Driver? stored = context.Drivers.Single(o => o.Id == entity.Id);
                if (stored == null) return;

                context.Drivers.Remove(stored);
                context.SaveChanges();
            }
        }

        public void Update(Driver entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                Driver? stored = context.Drivers.Single(o => o.Id == entity.Id);
                if (stored == null) return;

                stored = entity;
                context.Drivers.Update(stored);
                context.SaveChanges();
            }
        }

        public IEnumerable<Driver> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers.ToList();
            }
        }

        public Driver? GetById(int licenseId)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers.Single(o => o.Id == licenseId);
            }
        }

        public Driver? GetByPayrollNumber(string payrollNumber)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers.Single(o => o.PayrollNumber == payrollNumber);
            }
        }
    }
}
