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
                context.Passports.Add(entity);
                context.SaveChanges();
            }
        }
        public void Update(int id, Passport entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                Passport stored = context.Passports.Single(o => o.Id == id);

                stored = entity;
                stored.Id = id;

                context.Passports.Update(stored);
                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Passport stored = context.Passports.Single(o => o.Id == id);

                context.Passports.Remove(stored);
                context.SaveChanges();
            }
        }

        public Passport? GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Passports.SingleOrDefault(o => o.Id == id);
            }
        }

        public Passport? Get(string number, string series)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Passports.SingleOrDefault(x=>x.Number == number && x.Series == series);
            }
        }

    }
}
