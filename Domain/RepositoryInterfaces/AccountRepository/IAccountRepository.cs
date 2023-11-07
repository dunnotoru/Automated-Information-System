using Domain.Entities.Users;
using Domain.RepositoryInterfaces.AccountRepository.DTOs;

namespace Domain.RepositoryInterfaces.AccountRepository
{
    public interface IAccountRepository
    {
        Task<Account> CreateAsync(Account newAccount);
        Task<Account> GetByNameAsync(string name);
        Task<Account> GetByIdAsync(int id);
        Task<Account> UpdateAsync(AccountUpdateDTO accountUpdateDTO);
        Task<bool> DeleteAsync(int id);
    }
}