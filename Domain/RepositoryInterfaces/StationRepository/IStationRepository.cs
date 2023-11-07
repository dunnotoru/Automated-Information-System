using Domain.Models;

namespace Domain.RepositoryInterfaces.StationRepository
{
    public interface IStationRepository
    {
        Task AddAsync(Station addStationDTO);
        Task DeleteAsync(int id);
        Task<Station> GetByIdAsync(int id);
        Task<Station> GetByAddressAsync(string address);
        Task<List<Station>> GetByNameAsync(string name);
        Task UpdateAsync(Station station);
    }
}
