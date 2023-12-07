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

        public void Update(Route entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }

        public void Remove(Route entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Remove(entity);
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
                return context.Routes.Include(r=>r.Stations).ToList();
            }
        }

        public IEnumerable<Route> GetByStations(Station from, Station to)
        {
            ArgumentNullException.ThrowIfNull(from);
            ArgumentNullException.ThrowIfNull(to);

            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Routes.Where(r => r.Stations.Contains(from) && r.Stations.Contains(to))
                    .Include(r => r.Stations).ToList();
            }
        }
    }
}
