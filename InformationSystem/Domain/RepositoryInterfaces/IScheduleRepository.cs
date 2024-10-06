using System.Collections.Generic;
using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.RepositoryInterfaces;

public interface IScheduleRepository : IRepositoryBase<Schedule>
{
    Schedule GetByRun(Run run);
    IEnumerable<Schedule> GetByRoute(Route route);
}