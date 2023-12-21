using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Services
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IRunRepository _runRepository;

        public ScheduleService(IScheduleRepository scheduleRepository, IRunRepository runRepository)
        {
            _scheduleRepository = scheduleRepository;
            _runRepository = runRepository;
        }

        public void UpdateSchedule()
        {
            foreach (Schedule item in _scheduleRepository.GetAll())
            {
                Run run = item.Run;
                DateTime departure = run.DepartureDateTime;

                if (departure > DateTime.Now)
                {
                    run.DepartureDateTime.AddMinutes(item.PeriodInMinutes);
                    run.EstimatedArrivalDateTime.AddMinutes(item.PeriodInMinutes);
                }

                _runRepository.Update(run.Id, run);
            }
        }
    }
}
