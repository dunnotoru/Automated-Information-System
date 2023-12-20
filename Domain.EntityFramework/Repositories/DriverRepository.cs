using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                context.Drivers.Update(entity);

                DriverLicense storedLicense = context.Licenses.Single(o => o.Id == entity.License.Id);
                List<Category> categories = new List<Category>();
                foreach (var item in entity.License.Categories)
                {
                    if (context.Categories.Any(o => o.Id == item.Id))
                        categories.Add(context.Categories.Single(o => o.Id == item.Id));
                }
                storedLicense.Categories = categories;
                                
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
                return context.Drivers
                    .Include(o => o.License)
                    .ThenInclude(x => x.Categories)
                    .ToList();
            }
        }

        public Driver? GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers
                    .Include(o => o.License)
                    .ThenInclude(x => x.Categories)
                    .Single(o => o.Id == id);
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
