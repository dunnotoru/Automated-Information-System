using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.EntityFramework.Repositories
{
    public class StationRepository : IStationRepository
    {
        public bool Add(Station station)
        {
            try
            {
                using (DispatcherContext context = new DispatcherContext())
                {
                    context.Add(station);
                    context.SaveChanges();
                }
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (DispatcherContext context = new DispatcherContext())
                {
                    Station entity = context.Find<Station>(id);
                    if(entity != null)
                    {
                        context.Remove(entity);
                    }
                    context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Station> GetAll()
        {
            List<Station> result = new List<Station>();
            using (DispatcherContext context = new DispatcherContext())
            {
                result = context.Stations.ToList();
            }
            return result;
        }

        public Station Get(int id)
        {
            return Get(x => x.Id == id).FirstOrDefault();
        }

        public bool Update(Station station)
        {
            try
            {
                using (DispatcherContext context = new DispatcherContext())
                {
                    context.Update(station);
                    context.SaveChanges(true);
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public IEnumerable<Station> Get(Expression<Func<Station, bool>> where)
        {
            List<Station> result;
            using (DispatcherContext context = new DispatcherContext())
            {
                result = context.Stations.Where(where).ToList();
            }
            return result;
        }
    }
}
