using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        public void Add(Driver entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Licenses.Attach(entity.License);
                context.Drivers.Add(entity);
                
                context.SaveChanges();
            }
        }

        public void Update(int id, Driver entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                entity.Id = id;
                context.Drivers.Attach(entity);
                context.Licenses.Attach(entity.License);
                context.Drivers.Update(entity);
                context.Licenses.Update(entity.License);
                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Driver? stored = context.Drivers.Include(o => o.License).Single(o => o.Id == id);

                context.Drivers.Remove(stored);
                context.SaveChanges();
            }
        }


        public IEnumerable<Driver> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers.Include(o => o.License).ToList();
            }
        }

        public Driver? GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers.Include(o => o.License).Single(o => o.Id == id);
            }
        }

        public Driver? GetByPayrollNumber(string payrollNumber)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers.Include(o => o.License).Single(o => o.PayrollNumber == payrollNumber);
            }
        }
    }
}
