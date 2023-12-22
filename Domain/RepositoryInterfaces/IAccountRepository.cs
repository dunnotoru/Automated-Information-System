using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IAccountRepository : IRepositoryBase<Account>
    {
        Account GetByUsername(string username);
    }
}
