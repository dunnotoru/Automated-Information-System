using Domain.Models;

namespace Domain.RepositoryInterfaces.VehicleRepository
{
    public interface IVehicleRepository
    {
        Task AddAsync();
        Task DeleteAsync(string licensePlateNumber);
        Task UpdateRepairDataAsync(UpdateVehilceRepairDTO vehilceRepairDTO);
        Task<Vehicle> GetByLicensPlateNumberAsync(string licensePlateNumber);
    }
}
