using Domain.EntityFramework.Contexts;
using Domain.EntityFramework.Entities;
using Domain.EntityFramework.Mappers;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Domain.EntityFramework.Repositories
{
    public class StationRepository : IStationRepository
    {
        public bool AddAsync(Station run)
        {
            StationEntity entity = StationMapper.ToEntity(run);
            try
            {
                using (DispatcherContext context = new DispatcherContext())
                {
                    context.Add(entity);
                    context.SaveChanges();
                }
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public bool DeleteAsync(int id)
        {
            try
            {
                using (DispatcherContext context = new DispatcherContext())
                {
                    StationEntity entity = context.Find<StationEntity>(id);
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

        public IEnumerable<Station> GetAllAsync()
        {
            List<StationEntity> set = new List<StationEntity>();
            List<Station> result = new List<Station>();
            using (DispatcherContext context = new DispatcherContext())
            {
                set = context.Stations.ToList();
            }
            foreach (StationEntity entity in set) 
                result.Add(StationMapper.ToDomain(entity));

            return result;
        }

        public Station GetAsync(Func<Station,bool> predicate)
        {
            StationEntity entity;
            using (DispatcherContext context = new DispatcherContext())
            {
                entity = context.Stations.Where(predicate).ToList().FirstOrDefault();
            }
            return StationMapper.ToDomain(entity);
        }

        public IEnumerable<Station> GetByAddressAsync(string address)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAsync(Station run)
        {
            throw new NotImplementedException();
        }
    }
}
