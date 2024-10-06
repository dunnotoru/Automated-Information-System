namespace InformationSystem.Domain.Services.Abstractions;

public interface IPasswordValidator
{
    bool Validate(string value, string storedValue);
}