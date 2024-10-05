using Domain.Models;

namespace Domain.Services.Abstractions;

public interface ITicketPriceCalculator
{
    int CalcPrice(Run run, TicketType type);
}