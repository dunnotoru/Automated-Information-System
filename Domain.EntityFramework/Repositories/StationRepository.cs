﻿using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories;

public class StationRepository : IStationRepository
{
    private readonly IDbContextFactory<ApplicationContext> _factory;

    public StationRepository(IDbContextFactory<ApplicationContext> factory)
    {
        _factory = factory;
    }

    public int Create(Station entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            context.Stations.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Update(int id, Station entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            entity.Id = id;
            context.Stations.Attach(entity);
            context.Stations.Update(entity);
            context.SaveChanges();
        }
    }

    public void Remove(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            Station stored = context.Stations.Include(o => o.Routes).First(o => o.Id == id);
            if (stored.Routes.Count != 0)
            {
                string message = "";
                foreach(Route route in stored.Routes)
                {
                    message += route.Name + " ";
                }
                throw new InvalidOperationException($"Эта станция состоит в одном или нескольких маршрутах: {message}");
            }

            context.Remove(stored);
            context.SaveChanges();
        }
    }

    public IEnumerable<Station> GetAll()
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Stations.ToList();
        }
    }

    public IEnumerable<Station> GetByAddress(string address)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Stations.Where(s => s.Address == address).ToList();
        }
    }

    public Station GetById(int id)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Stations.First(s => s.Id == id);
        }
    }

    public IEnumerable<Station> GetByName(string name)
    {
        using (ApplicationContext context = _factory.CreateDbContext())
        {
            return context.Stations.Where(s => s.Name == name).ToList();
        }
    }
}