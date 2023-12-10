using Domain.Models;

namespace Domain.Services
{
    public interface ITicketPriceCalculator
    {
        int CalcPrice(Route route, Station from, Station to, TicketType type);
    }
}