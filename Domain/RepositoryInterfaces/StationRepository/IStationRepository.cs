using Domain.Models;

namespace Domain.RepositoryInterfaces.StationRepository
{
    public interface IStationRepository
    {
        Task<bool> AddAsync(AddStationDTO addStationDTO);
        Task<bool> DeleteAsync(int id);
        Task<Station> GetByIdAsync(int id);
        Task<Station> GetByAddressAsync(string address);
        Task<List<Station>> GetByNameAsync(string name);
        Task<bool> UpdateAsync(Station station);
    }
}
