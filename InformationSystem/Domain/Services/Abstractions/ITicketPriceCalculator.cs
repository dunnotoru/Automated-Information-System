using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.Services.Abstractions;

public interface ITicketPriceCalculator
{
    int CalcPrice(Run run, TicketType type);
}