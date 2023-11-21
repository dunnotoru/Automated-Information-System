using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IStationRepository : IRepositoryBase<Station>
    {
        Station? GetById(int id);
        IEnumerable<Station> GetByName(string name);
        IEnumerable<Station> GetByAddress(string address);
        IEnumerable<Station> GetAll();
    }
}
