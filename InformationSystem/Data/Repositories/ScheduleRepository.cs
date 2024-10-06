using System;
using System.Collections.Generic;
using System.Linq;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.Data.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly IDbContextFactory<DomainContext> _factory;

    public ScheduleRepository(IDbContextFactory<DomainContext> factory)
    {
        _factory = factory;
    }

    public int Create(Schedule entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (DomainContext context = _factory.CreateDbContext())
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
        using (DomainContext context = _factory.CreateDbContext())
        {
            Schedule stored = context.Schedules.First(o => o.Id == id);
            context.Schedules.Remove(stored);
            context.SaveChanges();
        }
    }

    public void Update(int id, Schedule entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (DomainContext context = _factory.CreateDbContext())
        {
            Schedule stored = context.Schedules.First(o => o.Id == id);
            context.Update(stored);
            context.SaveChanges();
        }
    }

    public Schedule GetById(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Schedules
                .Include(o=>o.Run)
                .Include(o => o.Route)
                .First(s => s.Id == id);
        }
    }

    public IEnumerable<Schedule> GetAll()
    {
        using (DomainContext context = _factory.CreateDbContext())
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
        using (DomainContext context = _factory.CreateDbContext())
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
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Schedules
                .Include(o => o.Run)
                .Include(o => o.Route)
                .First(s => s.RunId == run.Id);
        }
    }
}