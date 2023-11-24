using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class StationRepository : IStationRepository
    {
        private readonly CashierContext _context;

        public StationRepository(CashierContext context)
        {
            _context = context;
        }

        public void Add(Station entity)
        {
            _context.Add(entity);
        }

        public void Delete(Station entity)
        {
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
            _context.Update(entity);
        }
    }
}
