using Domain.Models;

namespace Domain.Services.Abstractions;

public interface IArrivalTimeCalculator
{
    DateTime Calculate(Route route, DateTime departureDateTime);
}