using Domain.Entities;
using Domain.RepositoryInterfaces.PassportRepository;

namespace Domain.EntityFramework.Repositories
{
    public class PassportRepository : IPassportRepository
    {
        public Task<bool> AddAsync(Passport passport)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int number, int series)
        {
            throw new NotImplementedException();
        }

        public Task<Passport> GetByNumberSeriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Passport passport)
        {
            throw new NotImplementedException();
        }
    }
}
