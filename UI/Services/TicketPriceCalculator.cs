using Domain.Models;

namespace Domain.Services
{
    internal class TicketPriceCalculator : ITicketPriceCalculator
    {
        public int CalcPrice(Route route, Station from, Station to, TicketType type)
        {
            return 1;
        }
    }
}
