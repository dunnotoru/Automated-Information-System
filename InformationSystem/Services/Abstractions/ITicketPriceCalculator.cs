using InformationSystem.Domain.Models;

namespace InformationSystem.Services.Abstractions;

public interface ITicketPriceCalculator
{
    int CalcPrice(Run run, TicketType type);
}