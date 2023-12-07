﻿using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Services
{
    public class RouteService
    {
        private readonly IRouteRepository _repository;

        public RouteService(IRouteRepository routeRepository)
        {
            _repository = routeRepository;
        }

        public void Add(Route route)
        {
            ArgumentNullException.ThrowIfNull(route);

            _repository.Add(route);
        }
        public void Update(Route route)
        {
            ArgumentNullException.ThrowIfNull(route);

            _repository.Update(route);
        }
        public void Delete(Route route)
        {
            if (route == null) return;
            Route? storedStation = _repository.GetById(route.Id);
            if (storedStation == null) return;
            _repository.Remove(route);
        }

        public Route? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Route> GetByStations(Station from, Station to)
        {
            IEnumerable<Route> routes = _repository.GetByStations(from, to);
            return routes;
        }

        public IEnumerable<Route> GetAll()
        {
            return _repository.GetAll();
        }

    }
}
