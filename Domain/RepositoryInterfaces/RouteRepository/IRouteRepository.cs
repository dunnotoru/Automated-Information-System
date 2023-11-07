using Domain.Models;

namespace Domain.RepositoryInterfaces.RouteRepository
{
    public interface IRouteRepository
    {
        Task AddAsync(Route route);
        Task<Route> GetByIdAsync(int id);
        Task<List<Route>> GetByNameAsync(string name);
        Task UpdateAsync(Route route);
        Task DeleteAsync(int id);
    }
}
