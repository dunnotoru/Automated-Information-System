using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.UseCases
{
    public class DriverUseCase
    {
        private readonly IDriverRepository _driverRepository;

        public DriverUseCase(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public bool AddDriver(Driver newDriver)
        {
            ArgumentNullException.ThrowIfNull(newDriver, nameof(newDriver));

            Driver? storedDriver = _driverRepository.GetByPayrollNumber(newDriver.PayrollNumber);
            if (storedDriver != null)
                return false;

            _driverRepository.Add(newDriver);

            try
            {
                _driverRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDriver(string payrollNumber)
        {
            Driver? stored = _driverRepository.GetByPayrollNumber(payrollNumber);
            if (stored == null)
                return false;

            _driverRepository.Remove(stored);

            try
            {
                _driverRepository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
