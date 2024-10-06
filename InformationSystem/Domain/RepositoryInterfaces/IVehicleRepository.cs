using System.Collections.Generic;
using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.RepositoryInterfaces;

public interface IVehicleRepository : IRepositoryBase<Vehicle>
{
    Vehicle GetByLicenseNumber(string licensePlateNumber);

    IEnumerable<Vehicle> GetIdleVehicles();
}