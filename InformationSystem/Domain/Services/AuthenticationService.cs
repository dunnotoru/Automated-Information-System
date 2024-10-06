using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;
using InformationSystem.Domain.Services.Abstractions;

namespace InformationSystem.Domain.Services;

public class AuthenticationService
{
    private readonly IPasswordValidator _passwordValidator;
    private readonly IAccountRepository _accountRepository;

    public AuthenticationService(IAccountRepository accountRepository,
        IPasswordValidator passwordValidator)
    {
        ArgumentNullException.ThrowIfNull(nameof(accountRepository));
        ArgumentNullException.ThrowIfNull(nameof(passwordValidator));

        _accountRepository = accountRepository;
        _passwordValidator = passwordValidator;
    }

    public Account Authenticate(string username, string password)
    {
        Account storedAccount;
        try
        {
            storedAccount = _accountRepository.GetByUsername(username);
        }
        catch
        {
            throw new InvalidOperationException("Пользователя с таким именем не существует");
        }

        if (_passwordValidator.Validate(password, storedAccount.PasswordHash))
            return storedAccount;
        else
            throw new InvalidOperationException("Неверные данные");
    }
}