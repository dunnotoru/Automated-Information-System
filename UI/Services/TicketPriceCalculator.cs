using Domain.Models;

namespace Domain.Services
{
    internal class TicketPriceCalculator : ITicketPriceCalculator
    {
        public int CalcPrice(Run run, TicketType type)
        {
            return 100 * run.Route.Stations.Count;
        }
    }
}
