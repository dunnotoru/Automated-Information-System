using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Services
{
    public class RouteService
    {
        private readonly IRouteRepository _routeRepository;

        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }

        public void Add(Route route)
        {
            _routeRepository.Add(route);
            _routeRepository.Save();
        }
        public void Update(Route route)
        {
            _routeRepository.Update(route);
            _routeRepository.Save();
        }
        public void Delete(Route route)
        {
            if (route == null) return;
            Route? storedStation = _routeRepository.GetById(route.Id);
            if (storedStation == null) return;
            _routeRepository.Delete(route);
            _routeRepository.Save();
        }

        public void AddStation(Route route, Station station)
        {
            if (route == null) return;
            if (station == null) return;
            Route? storedRoute = _routeRepository.GetById(route.Id);
            if(storedRoute == null) return; 
            storedRoute.Stations.Add(station);
            _routeRepository.Update(storedRoute);
        }

        public Route? GetById(int id)
        {
            return _routeRepository.GetById(id);
        }

        public IEnumerable<Route> GetByStations(Station from, Station to)
        {
            IEnumerable<Route> routes = _routeRepository.GetByStations(from, to);
            return routes;
        }

        public IEnumerable<Route> GetAll()
        {
            return _routeRepository.GetAll();
        }

    }
}
