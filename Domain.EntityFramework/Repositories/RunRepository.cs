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
            int id = 0;
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                Vehicle? b = context.Vehicles.First(o => o.Id == entity.Vehicle.Id);
                Route? route = context.Routes.First(o => o.Id == entity.Route.Id);
                Driver? driver = context.Drivers.First(o => o.Id == entity.Driver.Id);

                Run r = new Run()
                {
                    DepartureDateTime = entity.DepartureDateTime,
                    EstimatedArrivalDateTime = entity.EstimatedArrivalDateTime,
                    Vehicle = b,
                    Route = route,
                    Driver = driver,
                    Number = entity.Number,
                };


                context.Runs.Add(r);
                context.SaveChanges();

                id = r.Id;
            }
            return id;
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
                    .Include(o => o.Route).ThenInclude(x => x.Stations)
                    .Include(o => o.Vehicle).ThenInclude(x => x.VehicleModel)
                    .Include(o => o.Driver)
                    .First(x => x.Id == id);
            }
        }

        public IEnumerable<Run> GetByRoute(Route route)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Runs
                    .Include(o => o.Route).ThenInclude(x => x.Stations)
                    .Include(o => o.Vehicle).ThenInclude(x => x.VehicleModel)
                    .Include(o => o.Driver)
                    .Where(x => x.Route.Id == route.Id).ToList();
            }
        }

        public IEnumerable<Run> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Runs
                    .Include(o => o.Route).ThenInclude(x => x.Stations)
                    .Include(o => o.Vehicle).ThenInclude(x => x.VehicleModel)
                    .Include(o => o.Driver)
                    .ToList();
            }
        }

        public int GetFreePlaces(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                int takenPlaces = context.Tickets.Where(o => o.RunId == id).Count();
                int allPlaces = context.Runs
                    .Include(o => o.Vehicle).ThenInclude(x => x.VehicleModel)
                    .First(o => o.Id == id).Vehicle.VehicleModel.Capacity;

                return allPlaces - takenPlaces;
            }
        }
    }
}
