﻿using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Services
{
    public class RegistrationService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegistrationService(IAccountRepository accountRepository,
            IPasswordHasher passwordHasher)
        {
            _accountRepository = accountRepository;
            _passwordHasher = passwordHasher;
        }

        public bool Register(string username, string password,
            bool read = false, bool write = false, bool edit = false, bool delete = false)
        {
            Account? storedAccount = _accountRepository.GetByUsername(username);
            if (storedAccount == null) return false;

            string passwordHash = _passwordHasher.CalcHash(password);
            Account newAccount = new Account()
            {
                Username = username,
                PasswordHash = passwordHash,
                Read = read,
                Write = write,
                Edit = edit,
                Delete = delete
            };

            _accountRepository.Add(newAccount);

            return true;
        }

        public bool UpdatePassword(Account account, string oldPassword, string newPassword)
        {
            Account? storedAccount = _accountRepository.GetByUsername(account.Username);
            if (storedAccount == null) return false;
            if (storedAccount.PasswordHash != _passwordHasher.CalcHash(oldPassword)) return false;

            storedAccount.PasswordHash = _passwordHasher.CalcHash(newPassword);
            _accountRepository.Update(storedAccount.Id, storedAccount);
            return true;
        }
    }
}