using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class PassportRepository : IPassportRepository
    {
        public void Add(IdentityDocument entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Passports.Add(entity);
                context.SaveChanges();
            }
        }
        public void Update(int id, IdentityDocument entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                IdentityDocument stored = context.Passports.First(o => o.Id == id);

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
                IdentityDocument stored = context.Passports.First(o => o.Id == id);

                context.Passports.Remove(stored);
                context.SaveChanges();
            }
        }

        public IdentityDocument? GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Passports.FirstOrDefault(o => o.Id == id);
            }
        }

        public IdentityDocument? Get(string number, string series)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Passports.FirstOrDefault(x => x.Number == number && x.Series == series);
            }
        }

    }
}
