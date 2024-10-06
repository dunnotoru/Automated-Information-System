using System.Collections.Generic;
using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.RepositoryInterfaces;

public interface IDriverRepository : IRepositoryBase<Driver>
{
    Driver GetByPayrollNumber(string payrollNumber);
    IEnumerable<Driver> GetIdleDrivers();
}