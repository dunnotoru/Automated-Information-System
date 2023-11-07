using Domain.Models;

namespace Domain.RepositoryInterfaces.RouteRepository
{
    public interface IRouteRepository
    {
        Task<bool> AddAsync(AddRouteDTO addRouteDTO);
        Task<Route> GetByIdAsync(int id);
        Task<List<Route>> GetByNameAsync(string name);
        Task<bool> UpdateAsync(Route route);
        Task<bool> DeleteAsync(int id);
    }
}
