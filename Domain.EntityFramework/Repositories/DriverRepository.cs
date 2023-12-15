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
                Driver? stored = context.Drivers.Include(o => o.License).Single(o => o.Id == id);
                DriverLicense storedLicense = context.Licenses.Single(o => o.Id == entity.License.Id);

                stored = entity;
                stored.Id = id;

                storedLicense.LicenseNumber = stored.License.LicenseNumber;
                storedLicense.DateOfExpiration = stored.License.DateOfExpiration;
                storedLicense.DateOfIssue = stored.License.DateOfIssue;
                storedLicense.Categories = stored.License.Categories;

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
