using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Services
{
    public class RunService
    {
        private readonly IRunRepository _repository;

        public RunService(IRunRepository repository)
        {
            ArgumentNullException.ThrowIfNull(repository);
            _repository = repository;
        }

        public void Add(Run run)
        {
            ArgumentNullException.ThrowIfNull(run);
            _repository.Add(run);
        }

        public void Update(Run run)
        {
            ArgumentNullException.ThrowIfNull(run);
            _repository.Update(run);
        }

        public void Remove(Run run)
        {
            ArgumentNullException.ThrowIfNull(run);
            _repository.Remove(run);
        }

        public Run? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Run> GetByRoute(Route route)
        {
            ArgumentNullException.ThrowIfNull(route);

            return _repository.GetByRoute(route);
        }

        public IEnumerable<Run> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
