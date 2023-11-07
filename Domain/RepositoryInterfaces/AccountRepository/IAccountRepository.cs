using Domain.Models.Users;

namespace Domain.RepositoryInterfaces.AccountRepository
{
    public interface IAccountRepository
    {
        Task AddAsync(AddAccountDTO addAccountDTO);
        Task<Account> GetByNameAsync(string name);
        Task<Account> GetByIdAsync(int id);
        Task UpdateAsync(Account account);
        Task DeleteAsync(int id);
    }
}