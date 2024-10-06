using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.RepositoryInterfaces;

public interface IStationRepository : IRepositoryBase<Station>
{
    IEnumerable<Station> GetByName(string name);
    IEnumerable<Station> GetByAddress(string address);
}