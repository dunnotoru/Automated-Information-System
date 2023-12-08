using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Domain.EntityFramework.Repositories
{
    public class StationRepository : IStationRepository
    {
        public void Add(Station entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

            public void Update(Station entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Update(entity);
                context.SaveChanges();
            }
        }

        public void Remove(Station entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Remove(entity);
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

        public Station? GetById(int id)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                return context.Stations.SingleOrDefault(s => s.Id == id);
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
