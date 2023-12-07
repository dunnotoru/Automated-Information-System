using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace Domain.EntityFramework.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly ApplicationContext _context;

        public StationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Station entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Add(entity);
        }

        public void Remove(Station entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Remove(entity);
        }

        public IEnumerable<Station> GetAll()
        {
            return _context.Stations;
        }

        public IEnumerable<Station> GetByAddress(string address)
        {
            return _context.Stations.Where(s => s.Address == address);
        }

        public Station? GetById(int id)
        {
            return _context.Stations.SingleOrDefault(s => s.Id == id);
        }

        public IEnumerable<Station> GetByName(string name)
        {
            return _context.Stations.Where(s => s.Name == name);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Station entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Update(entity);
        }
    }
}
