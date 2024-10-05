using Domain.Models;

namespace Domain.RepositoryInterfaces;

public interface IRunRepository : IRepositoryBase<Run>
{
    IEnumerable<Run> GetByRoute(Route route);
    int GetFreePlaces(int id);
}