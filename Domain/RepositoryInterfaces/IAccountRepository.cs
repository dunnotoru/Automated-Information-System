using Domain.Models;

namespace Domain.RepositoryInterfaces;

public interface IAccountRepository : IRepositoryBase<Account>
{
    Account GetByUsername(string username);
    void UpdatePasswordHash(int id, string password_hash);
    bool IsAccountExist(string username);
    int Count();
}