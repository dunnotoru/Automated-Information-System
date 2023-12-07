using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Security.Cryptography;

namespace Domain.UseCases.CashierUseCases
{
    public class FindRunsUseCase
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IRunRepository _runRepository;

        public FindRunsUseCase(IRouteRepository routeRepository, IRunRepository runRepository)
        {
            _routeRepository = routeRepository;
            _runRepository = runRepository;
        }

        public IEnumerable<Run> FindRuns(Station from, Station to, DateTime fromDate)
        {
            return new List<Run>() { new Run() { Id = 228 } } ;
        }
    }
}
