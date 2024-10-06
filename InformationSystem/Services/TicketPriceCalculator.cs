using InformationSystem.Domain.Models;
using InformationSystem.Domain.Services.Abstractions;

namespace InformationSystem.Services;

internal class TicketPriceCalculator : ITicketPriceCalculator
{
    public int CalcPrice(Run run, TicketType type)
    {
        return 100 * run.Route.Stations.Count;
    }
}