using Domain.Models.Users;

namespace Domain.RepositoryInterfaces.AccountRepository
{
    public interface IAccountRepository
    {
        Task<bool> AddAsync(AddAccountDTO addAccountDTO);
        Task<Account> GetByNameAsync(string name);
        Task<Account> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Account account);
        Task<bool> DeleteAsync(int id);
    }
}