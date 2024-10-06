using InformationSystem.Domain.Services.Abstractions;

namespace InformationSystem.Services;

internal class PasswordValidator : IPasswordValidator
{
    private readonly IPasswordHasher _passwordHasher;

    public PasswordValidator(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public bool Validate(string value, string storedValue)
        => _passwordHasher.CalcHash(value) == storedValue;

}