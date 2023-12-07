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
            _context.Add(entity);
        }

        public void Delete(Route entity)
        {
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
            return _context.Routes.Where(r => r.Stations.Contains(from) && r.Stations.Contains(to));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Route entity)
        {
            _context.Update(entity);
        }
    }
}
