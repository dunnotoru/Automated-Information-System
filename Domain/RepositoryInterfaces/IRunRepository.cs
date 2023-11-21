﻿using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IRunRepository : IRepositoryBase<Run>
    {
        Run? GetByNumber(int id);
        IEnumerable<Run> GetByRoute(Route route);
    }
}