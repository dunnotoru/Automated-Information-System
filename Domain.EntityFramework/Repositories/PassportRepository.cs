using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class PassportRepository : IPassportRepository
    {
        public void Add(Passport entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using(ApplicationContext context = new ApplicationContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void Remove(Passport entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        public Passport? Get(string number, string series)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Passports.SingleOrDefault(x=>x.Number == number && x.Series == series);
            }
        }

        public void Update(Passport entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }
    }
}
