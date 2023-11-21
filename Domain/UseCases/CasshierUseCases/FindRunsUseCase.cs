using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Security.Cryptography;

namespace Domain.UseCases.CasshierUseCases
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
            IEnumerable<Route> routes = _routeRepository.GetByStations(from, to);
            IEnumerable<Run> runs = new List<Run>();
            foreach (Route route in routes)
            {
                runs = runs.Concat(_runRepository.GetByRoute(route));
            }
            
            return runs.Where(x => x.Departure == fromDate);
        }
    }
}
