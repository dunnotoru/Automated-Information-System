using Domain.Models;

namespace Domain.RepositoryInterfaces.PassportRepository
{
    public interface IPassportRepository
    {
        Task AddAsync(Passport passport);
        Task<Passport> GetByNumberSeriesAsync();
        Task UpdateAsync(Passport passport);
        Task DeleteAsync(int number, int series);
    }
}
