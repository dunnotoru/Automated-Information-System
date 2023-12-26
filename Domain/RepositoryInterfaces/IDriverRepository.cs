using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IDriverRepository : IRepositoryBase<Driver>
    {
        Driver GetByPayrollNumber(string payrollNumber);
        IEnumerable<Driver> GetIdleDrivers();
    }
}