using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly ApplicationContext _context;

        public RouteRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Route entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _context.Stations.AttachRange(entity.Stations);
            _context.Add(entity);
        }

        public void Remove(Route entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Remove(entity);
        }

        public Route? GetById(int id)
        {
            return _context.Routes.Include(r => r.Stations).SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<Route> GetAll()
        {
            return _context.Routes.Include(r=>r.Stations);
        }

        public IEnumerable<Route> GetByStations(Station from, Station to)
        {
            ArgumentNullException.ThrowIfNull(from);
            ArgumentNullException.ThrowIfNull(to);

            return _context.Routes.Where(r => r.Stations.Contains(from) && r.Stations.Contains(to))
                .Include(r => r.Stations);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Route entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Update(entity);
        }
    }
}
