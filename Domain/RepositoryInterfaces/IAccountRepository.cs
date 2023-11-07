using Domain.Users;

namespace Domain.RepositoryInterfaces
{
    public interface IAccountRepository
    {
        Task<Account> Create(AccountDTO accountDTO);
        Task<Account> Get();
        Task<Account> Update();
        Task<bool> Delete();
    }
}