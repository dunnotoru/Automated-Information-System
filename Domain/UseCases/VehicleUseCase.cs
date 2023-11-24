using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.UseCases
{
    public class VehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleUseCase(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public bool AddVehicle(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle, nameof(vehicle));

            Vehicle? stored = _vehicleRepository.GetByLicenseNumber(vehicle.LicensePlateNumber);
            if (stored != null)
                return false;

            _vehicleRepository.Add(vehicle);

            try
            {
                _vehicleRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
