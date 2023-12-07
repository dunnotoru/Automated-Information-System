using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class RunRepository : IRunRepository
    {
        public void Add(Run entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void Remove(Run entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Remove(entity);
                context.SaveChanges();
            }
        }

        public Run? GetById(int number)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Runs.SingleOrDefault(x=>x.Id == number);
            }
        }

        public IEnumerable<Run> GetByRoute(Route route)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Runs.Where(x => x.Route == route).ToList();
            }
        }

        public void Update(Run entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }

        public IEnumerable<Run> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Runs.ToList();
            }
        }
    }
}
