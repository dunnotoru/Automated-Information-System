using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IPassportRepository
    {
        bool AddAsync(Passport passport);
        bool UpdateAsync(Passport passport);
        bool DeleteAsync(int number, int series);
        Passport GetAsync(int number, int series);
        IEnumerable<Passport> GetAllAsync();
    }
}