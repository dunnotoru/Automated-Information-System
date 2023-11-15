namespace Domain.Services.AccountUseCases
{
    public interface IPasswordValidator
    {
        bool Validate(string value, string storedValue);
    }
}
