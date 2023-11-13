using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IStationRepository
    {
        bool AddAsync(Station run);
        bool UpdateAsync(Station run);
        bool DeleteAsync(int id);
        Station GetAsync(int id);
        IEnumerable<Station> GetAllAsync();
        IEnumerable<Station> GetByAddressAsync(string address);
        IEnumerable<Station> GetByNameAsync(string name);
    }
}
