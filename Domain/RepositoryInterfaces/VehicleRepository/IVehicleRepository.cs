using Domain.Models;

namespace Domain.RepositoryInterfaces.VehicleRepository
{
    public interface IVehicleRepository
    {
        Task<bool> AddAsync();
        Task<bool> DeleteAsync(string licensePlateNumber);
        Task<bool> UpdateRepairDataAsync(UpdateVehilceRepairDTO vehilceRepairDTO);
        Task<Vehicle> GetByLicensPlateNumberAsync(string licensePlateNumber);
    }
}
