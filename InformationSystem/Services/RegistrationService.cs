using System;
using System.Linq;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.Services;

public class RegistrationService
{
    private readonly IDbContextFactory<DomainContext> _contextFactory;
    private readonly IPasswordHasher _passwordHasher;

    public RegistrationService(IPasswordHasher passwordHasher, IDbContextFactory<DomainContext> contextFactory)
    {
        _passwordHasher = passwordHasher;
        _contextFactory = contextFactory;
    }

    public int Register(string username, string password,
        bool read = false, bool write = false, bool edit = false, bool delete = false)
    {
        bool isExist = false;

        using (var context = _contextFactory.CreateDbContext())
        {
            isExist = context.Accounts.FirstOrDefault(a => a.Username == username) is not null;
        }
        
        if (isExist)
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

        using (DomainContext context = _contextFactory.CreateDbContext())
        {
            context.Accounts.Add(newAccount);
            context.SaveChanges();
        }
        
        return newAccount.Id;
    }

    public bool UpdatePassword(string username, string oldPassword, string newPassword)
    {
        return false;
    }
}