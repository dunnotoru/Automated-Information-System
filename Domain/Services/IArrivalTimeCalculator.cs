using Domain.Models;

namespace Domain.Services
{
    public interface IArrivalTimeCalculator
    {
        DateTime Calculate(Route route, DateTime departureDateTime);
    }
}
