using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IPassportRepository : IRepositoryBase<Passport>
    {
        Passport? Get(string number, string series);
    }
}