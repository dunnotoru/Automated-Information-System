﻿using Domain.Entities.Users;
using Domain.RepositoryInterfaces.AccountRepository.DTOs;

namespace Domain.RepositoryInterfaces.AccountRepository
{
    public interface IAccountRepository
    {
        Task<bool> AddAsync(AccountDTO accountDTO);
        Task<Account> GetByNameAsync(string name);
        Task<Account> GetByIdAsync(int id);
        Task<bool> UpdateAsync(AccountDTO accountDTO);
        Task<bool> DeleteAsync(int id);
    }
}