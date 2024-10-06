using System;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;
using InformationSystem.Domain.Services.Abstractions;

namespace InformationSystem.Domain.Services;

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

    public int Register(string username, string password,
        bool read = false, bool write = false, bool edit = false, bool delete = false)
    {
        bool exist = _accountRepository.IsAccountExist(username);
        if (exist)
        {
            throw new InvalidOperationException("Аккаунт с таким именем уже существует.");
        }

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

        return _accountRepository.Create(newAccount);
    }

    public bool UpdatePassword(string username, string oldPassword, string newPassword)
    {
        Account storedAccount = null;
        try
        {
            storedAccount = _accountRepository.GetByUsername(username);
        }
        catch
        {
            storedAccount = null;
        }

        if (storedAccount == null) return false;
        if (storedAccount.PasswordHash != _passwordHasher.CalcHash(oldPassword)) return false;

        string passwordHash = _passwordHasher.CalcHash(newPassword);
        try
        {
            _accountRepository.UpdatePasswordHash(storedAccount.Id, passwordHash);
        }
        catch
        { 
            return false; 
        }

        return true;
    }
}