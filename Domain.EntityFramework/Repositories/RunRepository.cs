using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Diagnostics.Contracts;

namespace Domain.EntityFramework.Repositories
{
    public class RunRepository : IRunRepository
    {
        public void Add(Run entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                Vehicle? b = context.Set<Vehicle>().SingleOrDefault(o => o.Id == entity.Bus.Id);
                Route? route = context.Set<Route>().SingleOrDefault(o => o.Id == entity.Route.Id);
                ICollection<Driver> drivers = context.Set<Driver>().ToList();

                if (b == null) return;
                if (route == null) return; 
                if (drivers == null) return;

                Run r = new Run()
                {
                    Id = entity.Id,
                    Departure = entity.Departure,
                    EstimatedArrival = entity.EstimatedArrival,
                    Bus = b,
                    Route = route,
                    Drivers = drivers
                };

                context.Runs.Add(r);
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
