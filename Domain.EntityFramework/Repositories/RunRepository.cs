using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class RunRepository : IRunRepository
    {
        private readonly ApplicationContext _context;

        public RunRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Add(Run entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Add(entity);
        }

        public void Remove(Run entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Remove(entity);
        }

        public Run? GetById(int number)
        {
            return _context.Runs.SingleOrDefault(x=>x.Id == number);
        }

        public IEnumerable<Run> GetByRoute(Route route)
        {
            return _context.Runs.Where(x => x.Route == route);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Run entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            _context.Update(entity);
        }

        public IEnumerable<Run> GetAll()
        {
            return _context.Runs;
        }
    }
}
