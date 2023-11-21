using Domain.Models.Drivers;
using Domain.RepositoryInterfaces;

namespace Domain.UseCases.HumanResoursesUseCases
{
    public class AddDriverUseCase
    {
        private readonly IDriverRepository _driverRepository;

        public AddDriverUseCase(IDriverRepository driverRepository)
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
    }
}
