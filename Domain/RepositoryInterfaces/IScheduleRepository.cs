using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IScheduleRepository : IRepositoryBase<Schedule>
    {
        Schedule? GetById(int id);
        Schedule? GetByRun(Run run);
        IEnumerable<Schedule> GetByRoute(Route route);
        IEnumerable<Schedule> GetAll();
    }
}
