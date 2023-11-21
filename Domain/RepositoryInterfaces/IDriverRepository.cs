using Domain.Models.Drivers;

namespace Domain.RepositoryInterfaces
{
    public interface IDriverRepository : IRepositoryBase<Driver>
    {
        Driver? GetByPayrollNumber(string payrollNumber);
        Driver? GetByLicenseId(string licenseId);

    }
}