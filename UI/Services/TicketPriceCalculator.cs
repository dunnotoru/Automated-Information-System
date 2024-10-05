using Domain.Models;
using Domain.Services.Abstractions;

namespace UI.Services;

internal class TicketPriceCalculator : ITicketPriceCalculator
{
    public int CalcPrice(Run run, TicketType type)
    {
        return 100 * run.Route.Stations.Count;
    }
}