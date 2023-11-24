using Domain.Models;

namespace Domain.UseCases.CasshierUseCases
{
    public interface ITicketPriceCalculator
    {
        int CalcPrice(Route route, Station from, Station to, TicketType type);
    }
}