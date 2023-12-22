using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IScheduleRepository : IRepositoryBase<Schedule>
    {
        Schedule GetByRun(Run run);
        IEnumerable<Schedule> GetByRoute(Route route);
    }
}
