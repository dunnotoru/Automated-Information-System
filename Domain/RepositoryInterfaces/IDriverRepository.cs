using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IDriverRepository : IRepositoryBase<Driver>
    {
        Driver? GetByPayrollNumber(string payrollNumber);
        Driver? GetById(int licenseId);
        IEnumerable<Driver> GetAll();
    }
}