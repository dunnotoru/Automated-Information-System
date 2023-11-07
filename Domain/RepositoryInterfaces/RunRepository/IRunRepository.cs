using Domain.Models;

namespace Domain.RepositoryInterfaces.RunRepository
{
    public interface IRunRepository
    {
        Task AddAsync();
        Task<Run> GetByIdAsync(int Id);
        Task<Run> GetByTargetPlaceAsync(Station station);
        Task<Run> GetByDeparturePlaceAsync(Station station);
        Task<Run> GetByDepartureDateTimeAsync(DateTime departureTime);
        Task<Run> GetByArrivalDateTimeAsync(DateTime departureTime);
        Task<Run> GetByDriverAsync(DateTime departureTime);
        Task DeleteAsync(int id);
    }
}
