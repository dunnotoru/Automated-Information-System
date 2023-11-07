﻿using Domain.Models.Users;
using Domain.EntityFramework.Entities;
using Domain.RepositoryInterfaces.AccountRepository;
using Microsoft.EntityFrameworkCore;
using Domain.EntityFramework.Mappers;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Update;

namespace Domain.EntityFramework.Repositories.AccountRepositoryImpl
{
    public class AccountRepository : IAccountRepository
    {
        private string _connectionString;

        public AccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task AddAsync(AddAccountDTO addAccountDTO)
        {
            
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Account> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetByNameAsync(string name)
        {
            AccountEntity? accountEntity;
            using (AccountContext db = new AccountContext(_connectionString))
            {
                accountEntity = await db.Accounts.FirstOrDefaultAsync(_ => _.Name == name);
            }

            if (accountEntity == null)
                throw new NullReferenceException();

            return AccountMapper.ToDomain(accountEntity);
        }

        public Task UpdateAsync(Account account)
        {
            throw new NotImplementedException();
        }
    }
}
