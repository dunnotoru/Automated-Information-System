using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private ApplicationContext _context;

        public ScheduleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Schedule entity)
        {
            _context.Add(entity);
        }

        public void Delete(Schedule entity)
        {
            _context.Schedules.Remove(entity);
        }
        public void Update(Schedule entity)
        {
            _context.Update(entity);
        }

        public Schedule? GetById(int id)
        {
            return _context.Schedules.SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<Schedule> GetAll()
        {
            return _context.Schedules;
        }

        public IEnumerable<Schedule> GetByRoute(Route route)
        {
            ArgumentNullException.ThrowIfNull(route);
            return _context.Schedules.Where(s => s.Route == route);
        }

        public Schedule? GetByRun(Run run)
        {
            ArgumentNullException.ThrowIfNull(run);
            return _context.Schedules.SingleOrDefault(s => s.Run == run);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
