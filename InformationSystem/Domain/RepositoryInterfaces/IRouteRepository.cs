using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.RepositoryInterfaces;

public interface IRouteRepository : IRepositoryBase<Route>
{
    IEnumerable<Route> GetByStations(Station from, Station to);
}