using Domain.Models;

namespace Domain.Services
{
    public interface ITicketPriceCalculator
    {
        int CalcPrice(Run run, TicketType type);
    }
}