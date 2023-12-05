using Domain.Models;

namespace Domain.UseCases.CashierUseCases
{
    public interface ITicketPriceCalculator
    {
        int CalcPrice(Route route, Station from, Station to, TicketType type);
    }
}