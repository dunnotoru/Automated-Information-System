using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories
{
    public class RunRepository : IRunRepository
    {
        public int Create(Run entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                Vehicle? b = context.Vehicles.First(o => o.Id == entity.Vehicle.Id);
                Route? route = context.Routes.First(o => o.Id == entity.Route.Id);
                ICollection<Driver> drivers = context.Drivers.ToList();

                Run r = new Run()
                {
                    Id = entity.Id,
                    DepartureDateTime = entity.DepartureDateTime,
                    EstimatedArrivalDateTime = entity.EstimatedArrivalDateTime,
                    Vehicle = b,
                    Route = route,
                    Drivers = drivers,
                    Number = entity.Number,
                };

                context.Runs.Add(r);
                context.SaveChanges();
            }
            return entity.Id;
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Run stored = context.Runs.First(o => o.Id == id);
                context.Remove(stored);
                context.SaveChanges();
            }
        }

        public void Update(int id, Run entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                entity.Id = id;
                context.Attach(entity);
                context.Update(entity);
                context.SaveChanges();
            }
        }

        public Run GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Runs
                    .Include(o => o.Route)
                    .ThenInclude(x => x.Stations)
                    .Include(o => o.Vehicle)
                    .Include(o => o.Drivers)
                    .First(x => x.Id == id);
            }
        }

        public IEnumerable<Run> GetByRoute(Route route)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Runs
                    .Include(o => o.Route)
                    .ThenInclude(x => x.Stations)
                    .Include(o => o.Vehicle)
                    .Include(o => o.Drivers)
                    .Where(x => x.Route.Id == route.Id).ToList();
            }
        }

        public IEnumerable<Run> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Runs
                    .Include(o => o.Route)
                    .ThenInclude(x => x.Stations)
                    .Include(o => o.Vehicle)
                    .Include(o => o.Drivers)
                    .ToList();
            }
        }
    }
}
