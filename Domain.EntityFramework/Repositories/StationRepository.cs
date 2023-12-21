﻿using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class StationRepository : IStationRepository
    {
        public void Add(Station entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Stations.Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(int id, Station entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                entity.Id = id;
                context.Stations.Attach(entity);
                context.Stations.Update(entity);
                context.SaveChanges();
            }
        }

        public void Remove(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                Station stored = context.Stations.First(o => o.Id == id);
                context.Remove(stored);
                context.SaveChanges();
            }
        }

        public IEnumerable<Station> GetAll()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Stations.ToList();
            }
        }

        public IEnumerable<Station> GetByAddress(string address)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Stations.Where(s => s.Address == address).ToList();
            }
        }

        public Station GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Stations.First(s => s.Id == id);
            }
        }

        public IEnumerable<Station> GetByName(string name)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Stations.Where(s => s.Name == name).ToList();
            }
        }
    }
}
