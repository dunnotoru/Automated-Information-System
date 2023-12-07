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
            _repository.Save();
        }

        public void Update(Run run)
        {
            ArgumentNullException.ThrowIfNull(run);
            _repository.Update(run);
            _repository.Save();
        }

        public void Remove(Run run)
        {
            ArgumentNullException.ThrowIfNull(run);
            _repository.Remove(run);
            _repository.Save();
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
    }
}
