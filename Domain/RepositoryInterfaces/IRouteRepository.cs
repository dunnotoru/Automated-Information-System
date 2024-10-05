using Domain.Models;

namespace Domain.RepositoryInterfaces;

public interface IRouteRepository : IRepositoryBase<Route>
{
    IEnumerable<Route> GetByStations(Station from, Station to);
}