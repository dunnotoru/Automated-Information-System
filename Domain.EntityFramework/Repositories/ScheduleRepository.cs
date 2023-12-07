using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        public ScheduleRepository()
        {
            
        }

        public void Add(Schedule entity)
        {
            ArgumentNullException.ThrowIfNull(entity);

            using (ApplicationContext context = new ApplicationContext())
            {
                context.Add(entity);
            }
        }

        public void Remove(Schedule entity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Schedules.Remove(entity);
            }
        }
        public void Update(Schedule entity)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Update(entity);
            }
        }

        public Schedule? GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Schedules.SingleOrDefault(s => s.Id == id);
            }
        }

        public IEnumerable<Schedule> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Schedules;
            }
        }

        public IEnumerable<Schedule> GetByRoute(Route route)
        {
            ArgumentNullException.ThrowIfNull(route);
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Schedules.Where(s => s.Route == route);
            }
        }

        public Schedule? GetByRun(Run run)
        {
            ArgumentNullException.ThrowIfNull(run);
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Schedules.SingleOrDefault(s => s.Run == run);
            }
        }
    }
}
