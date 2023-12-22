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
            List<Schedule> schedules = _scheduleRepository.GetAll().ToList();
            foreach (Run item in _runRepository.GetAll())
            {
                if (item.DepartureDateTime > DateTime.Now)
                {
                    item.DepartureDateTime.AddMinutes(30);
                    item.EstimatedArrivalDateTime.AddMinutes(30);
                }

                if (schedules.Any(o => o.RunId == item.Id))
                    _scheduleRepository.Create(new Schedule()
                    {
                        PeriodInMinutes = 30,
                        Run = item,
                        Route = item.Route
                    });

                _runRepository.Update(item.Id, item);
            }
        }
    }
}
