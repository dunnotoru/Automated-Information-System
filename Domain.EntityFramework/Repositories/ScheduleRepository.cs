using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    public int Create(Schedule entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = new ApplicationContext())
        {
            context.Attach(entity.Run);
            entity.Route = entity.Run.Route;
            context.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            Schedule stored = context.Schedules.First(o => o.Id == id);
            context.Schedules.Remove(stored);
            context.SaveChanges();
        }
    }

    public void Update(int id, Schedule entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = new ApplicationContext())
        {
            Schedule stored = context.Schedules.First(o => o.Id == id);
            context.Update(stored);
            context.SaveChanges();
        }
    }

    public Schedule GetById(int id)
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            return context.Schedules
                .Include(o=>o.Run)
                .Include(o => o.Route)
                .First(s => s.Id == id);
        }
    }

    public IEnumerable<Schedule> GetAll()
    {
        using (ApplicationContext context = new ApplicationContext())
        {
            return context.Schedules
                .Include(o => o.Run)
                .ThenInclude(o => o.Driver)
                .Include(o => o.Run)
                .ThenInclude(o => o.Vehicle)
                .Include(o => o.Route)
                .ToList();
        }
    }

    public IEnumerable<Schedule> GetByRoute(Route route)
    {
        ArgumentNullException.ThrowIfNull(route);
        using (ApplicationContext context = new ApplicationContext())
        {
            return context.Schedules
                .Include(o => o.Run)
                .Include(o => o.Route)
                .Where(s => s.Route == route)
                .ToList();
        }
    }

    public Schedule GetByRun(Run run)
    {
        ArgumentNullException.ThrowIfNull(run);
        using (ApplicationContext context = new ApplicationContext())
        {
            return context.Schedules
                .Include(o => o.Run)
                .Include(o => o.Route)
                .First(s => s.RunId == run.Id);
        }
    }
}