using Domain.EntityFramework.Contexts;
using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.EntityFramework.Repositories
{
    public class RunRepository : IRunRepository
    {
        private readonly CasshierContext _context;

        public RunRepository(CasshierContext context)
        {
            _context = context;
        }

        public void Add(Run entity)
        {
            _context.Add(entity);
        }

        public void Delete(Run entity)
        {
            _context.Remove(entity);
        }

        public Run? GetByNumber(int number)
        {
            return _context.Runs.SingleOrDefault(x=>x.Number == number);
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
            _context.Update(entity);
        }
    }
}
