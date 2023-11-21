using Domain.Models;

namespace Domain.UseCases.CasshierUseCases
{
    public class TicketPriceCalculator : ITicketPriceCalculator
    {
        public int CalcPrice(Route route, Station from, Station to, TicketType type)
        {
            return 1;
        }
    }
}
