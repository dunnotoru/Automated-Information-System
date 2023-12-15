using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IVehicleRepository : IRepositoryBase<Vehicle>
    {
        Vehicle GetById(int id);
        Vehicle GetByLicenseNumber(string licensePlateNumber);
        IEnumerable<Vehicle> GetAll();
    }
}