using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class RouteRepository : IRouteRepository
    {
        private readonly CasshierContext _context;

        public RouteRepository(CasshierContext context)
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
            return _context.Routes.SingleOrDefault(r => r.Id == id);
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
