using System;
using System.Collections.Generic;
using System.Linq;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;

namespace InformationSystem.Domain.Services;

public class RunSearchService
{
    private readonly IRunRepository _runRepository;
    private readonly IRouteRepository _routeRepository;
    private readonly ITicketRepository _ticketRepository;

    public RunSearchService(IRunRepository runRepository, IRouteRepository routeRepository, ITicketRepository ticketRepository)
    {
        _runRepository = runRepository;
        _routeRepository = routeRepository;
        _ticketRepository = ticketRepository;
    }

    public List<Run> GetAvailableRuns(Station departureStation, Station arrivalStation, DateTime departureDateTimeMinimum,
        DateTime departureDateTimeMaximum)
    {
        List<Run> runs = new List<Run>();
        IEnumerable<Route> routes;

        routes = _routeRepository.GetByStations(departureStation, arrivalStation);
        foreach (Route route in routes)
        {
            runs.AddRange(_runRepository.GetByRoute(route));
        }

        runs = runs.Where(o => o.DepartureDateTime > departureDateTimeMinimum
                               && o.DepartureDateTime < departureDateTimeMaximum).ToList();

        runs = runs.Where(o => o.EstimatedArrivalDateTime > DateTime.Now).ToList();
        runs = runs.Where(o => o.DepartureDateTime > DateTime.Now).ToList();

        runs = runs.Where(o => _runRepository.GetFreePlaces(o.Id) > 0).ToList();

        return runs;
    }
}