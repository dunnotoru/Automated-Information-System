using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Tests.FindRunsTest
{
    internal class RunRepositoryStub : IRunRepository
    {
        private HashSet<Run> _runs;

        public RunRepositoryStub(HashSet<Run> runs)
        {
            _runs = runs;
        }

        public void Add(Run entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Run entity)
        {
            throw new NotImplementedException();
        }

        public Run? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Run> GetByRoute(Route route)
        {
            return _runs.Where(x=>x.Route == route).ToList();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Run entity)
        {
            throw new NotImplementedException();
        }
    }
}
