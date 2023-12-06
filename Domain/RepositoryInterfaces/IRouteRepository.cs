using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IRouteRepository : IRepositoryBase<Route>
    {
        Route? GetById(int id);
        IEnumerable<Route> GetByStations(Station from, Station to);
        IEnumerable<Route> GetAll();
    }
}