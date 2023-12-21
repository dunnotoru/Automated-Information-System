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
                context.Categories.AttachRange(entity.DriverLicense.Categories);
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

                List<LicenseCategory> stored = context
                    .LicenseCategory
                    .Where(o => o.DriverLicenseId == entity.DriverLicense.Id)
                    .ToList();

                context.LicenseCategory.RemoveRange(stored);

                List<Category> categories = new List<Category>();
                foreach (Category category in entity.DriverLicense.Categories)
                {
                    LicenseCategory lc = new LicenseCategory()
                    {
                        DriverLicenseId = entity.DriverLicense.Id,
                        CategoryId = category.Id
                    };

                    context.LicenseCategory.Add(lc);
                }

                context.Drivers.Attach(entity);
                context.Licenses.Attach(entity.DriverLicense);
                context.Drivers.Update(entity);
                context.Licenses.Update(entity.DriverLicense);

                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Driver? stored = context.Drivers.Include(o => o.DriverLicense).Single(o => o.Id == id);

                context.Drivers.Remove(stored);
                context.SaveChanges();
            }
        }


        public IEnumerable<Driver> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers
                    .Include(o => o.DriverLicense)
                    .ThenInclude(x => x.Categories)
                    .ToList();
            }
        }

        public Driver? GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers
                    .Include(o => o.DriverLicense)
                    .ThenInclude(x => x.Categories)
                    .Single(o => o.Id == id);
            }
        }

        public Driver? GetByPayrollNumber(string payrollNumber)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers.Include(o => o.DriverLicense).Single(o => o.PayrollNumber == payrollNumber);
            }
        }
    }
}
