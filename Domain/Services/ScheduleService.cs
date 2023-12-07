using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Services
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _repository;

        public ScheduleService(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public void Add(Schedule schedule)
        {
            _repository.Add(schedule);
            _repository.Save();
        }

        public void Update(Schedule schedule)
        {
            _repository.Update(schedule);
            _repository.Save();
        }

        public void Delete(Schedule schedule)
        {
            if (schedule == null) return;
            Schedule? storedStation = _repository.GetById(schedule.Id);
            if (storedStation == null) return;
            _repository.Delete(schedule);
            _repository.Save();
        }

        public Schedule? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Schedule> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Schedule> GetByRoute(Route route)
        {
            return _repository.GetByRoute(route);
        }

        public Schedule? GetByRun(Run run)
        {
            return _repository.GetByRun(run);
        }
    }
}
