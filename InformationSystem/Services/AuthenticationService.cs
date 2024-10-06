using System;
using System.Linq;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.Domain.Services;

public class AuthenticationService
{
    private readonly IPasswordValidator _passwordValidator;
    private readonly IDbContextFactory<DomainContext> _contextFactory;

    public AuthenticationService(IPasswordValidator passwordValidator, IDbContextFactory<DomainContext> contextFactory)
    {
        _passwordValidator = passwordValidator;
        _contextFactory = contextFactory;
    }

    public Account Authenticate(string username, string password)
    {
        Account? storedAccount;

        using (DomainContext context = _contextFactory.CreateDbContext())
        {
            storedAccount = context.Accounts.FirstOrDefault(o => o.Username == username);
        }

        if (storedAccount == null)
        {
            
        }

        if (_passwordValidator.Validate(password, storedAccount.PasswordHash))
            return storedAccount;
        else
            throw new InvalidOperationException("Неверные данные");
    }
}