using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Domain.EntityFramework.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        public int Create(Driver entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Categories.AttachRange(entity.DriverLicense.Categories);
                context.Drivers.Add(entity);
                context.SaveChanges();
            }
            return entity.Id;
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
                Driver? stored = context.Drivers.Include(o => o.DriverLicense).Include(o => o.Run).First(o => o.Id == id);
                if (stored.Run != null)
                {
                    string message = stored.Run.Number + " ";
                    throw new InvalidOperationException($"Этот водитель назначен на рейс: {message}");
                }
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

        public Driver GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers
                    .Include(o => o.DriverLicense)
                    .ThenInclude(x => x.Categories)
                    .First(o => o.Id == id);
            }
        }

        public Driver GetByPayrollNumber(string payrollNumber)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Drivers
                    .Include(o => o.DriverLicense)
                    .First(o => o.PayrollNumber == payrollNumber);
            }
        }

        public IEnumerable<Driver> GetIdleDrivers()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                try
                {
                    return context.Drivers
                        .Include(o => o.DriverLicense)
                        .ThenInclude(x => x.Categories)
                        .Where(o => o.Run == null)
                        .ToList();
                }
                catch
                {
                    return new List<Driver>();
                }
            }
        }
    }
}
