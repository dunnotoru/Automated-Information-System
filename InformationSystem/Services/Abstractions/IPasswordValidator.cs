namespace InformationSystem.Services.Abstractions;

public interface IPasswordValidator
{
    bool Validate(string value, string storedValue);
}