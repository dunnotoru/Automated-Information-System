using System;
using System.Collections.Generic;
using System.Linq;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.Data.Repositories;

public class StationRepository : IStationRepository
{
    private readonly IDbContextFactory<DomainContext> _factory;

    public StationRepository(IDbContextFactory<DomainContext> factory)
    {
        _factory = factory;
    }

    public int Create(Station entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (DomainContext context = _factory.CreateDbContext())
        {
            context.Stations.Add(entity);
            context.SaveChanges();
        }
        return entity.Id;
    }

    public void Update(int id, Station entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        using (DomainContext context = _factory.CreateDbContext())
        {
            entity.Id = id;
            context.Stations.Attach(entity);
            context.Stations.Update(entity);
            context.SaveChanges();
        }
    }

    public void Remove(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
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
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Stations.ToList();
        }
    }

    public IEnumerable<Station> GetByAddress(string address)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Stations.Where(s => s.Address == address).ToList();
        }
    }

    public Station GetById(int id)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Stations.First(s => s.Id == id);
        }
    }

    public IEnumerable<Station> GetByName(string name)
    {
        using (DomainContext context = _factory.CreateDbContext())
        {
            return context.Stations.Where(s => s.Name == name).ToList();
        }
    }
}