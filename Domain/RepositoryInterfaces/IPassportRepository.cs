using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IPassportRepository : IRepositoryBase<Passport>
    {
        Passport? Get(int number, int series);
    }
}