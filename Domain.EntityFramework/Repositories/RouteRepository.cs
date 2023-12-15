using Domain.EntityFramework.Configurations;
using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        public void Add(Route entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Stations.AttachRange(entity.Stations);
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(int id, Route entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                Route? stored = context.Routes.Single(r => r.Id == id);
                
                stored.Stations.Clear();
                
                IEnumerable<StationRoute> toRemove = context.Set<StationRoute>()
                    .Where(_ => _.RouteId == stored.Id);
                context.Set<StationRoute>().RemoveRange(toRemove);
                context.SaveChanges();

                stored.Name = entity.Name;
                stored.Stations = entity.Stations;
                stored.Id = id;

                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Route? stored = context.Routes.Single(r => r.Id == id);
                context.Remove(stored);
                context.SaveChanges();
            }
        }

        public Route? GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Routes.Include(r => r.Stations).SingleOrDefault(r => r.Id == id);
            }
        }

        public IEnumerable<Route> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Routes.Include(r => r.Stations).ToList();
            }
        }

        public IEnumerable<Route> GetByStations(Station from, Station to)
        {
            ArgumentNullException.ThrowIfNull(from);
            ArgumentNullException.ThrowIfNull(to);

            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Routes.Include(r => r.Stations)
                    .Where(r => r.Stations.Contains(from) && r.Stations.Contains(to))
                    .ToList();
            }
        }
    }
}
