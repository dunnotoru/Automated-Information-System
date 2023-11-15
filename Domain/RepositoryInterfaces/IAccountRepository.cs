using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Account? GetById(int id);
        Account? GetByUsername(string username);
    }
}
