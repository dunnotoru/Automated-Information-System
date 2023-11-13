using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IRunRepository
    {
        bool AddAsync(Run run);
        bool UpdateAsync(Run run);
        bool DeleteAsync(int id);
        Run GetAllAsync();
        Run GetAsync(int id);
        IEnumerable<Run> GetByDepartureAsync(Station station, DateOnly date);
        IEnumerable<Run> GetByArrivalAsync(Station station);
            
    }
}