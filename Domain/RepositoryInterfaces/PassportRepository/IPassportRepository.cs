using Domain.Entities;

namespace Domain.RepositoryInterfaces.PassportRepository
{
    public interface IPassportRepository
    {
        Task<bool> AddAsync(Passport passport);
        Task<Passport> GetByNumberSeriesAsync();
        Task<bool> UpdateAsync(Passport passport);
        Task<bool> DeleteAsync(int number, int series);
    }
}
