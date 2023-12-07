using Domain.Models;
using Domain.RepositoryInterfaces;
using NuGet.Frameworks;

namespace Domain.Tests.FindRunsTest
{
    internal class RouteRepositoryStub : IRouteRepository
    {
        private HashSet<Route> _routes;

        public RouteRepositoryStub(HashSet<Route> routes)
        {
            _routes = routes;
        }

        public void Add(Route entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Route entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route> GetAll()
        {
            throw new NotImplementedException();
        }

        public Route? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route> GetByStations(Station from, Station to)
        {
            return _routes.Where(x=>x.Stations.Contains(from) && x.Stations.Contains(to));
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Route entity)
        {
            throw new NotImplementedException();
        }
    }
}
