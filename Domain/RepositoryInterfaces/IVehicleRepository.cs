using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IVehicleRepository : IRepositoryBase<Vehicle>
    {
        Vehicle GetByLicenseNumber(string licensePlateNumber);

        IEnumerable<Vehicle> GetIdleVehicles();
    }
}