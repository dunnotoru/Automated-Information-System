using Domain.Models.Drivers;
using Domain.RepositoryInterfaces;

namespace Domain.UseCases.HumanResoursesUseCases
{
    public class DeleteDriverUseCase
    {
        private readonly IDriverRepository _driverRepository;

        public DeleteDriverUseCase(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public bool DeleteDriver(string payrollNumber)
        {
            Driver? stored = _driverRepository.GetByPayrollNumber(payrollNumber);
            if (stored == null)
                return false;

            _driverRepository.Delete(stored);

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
