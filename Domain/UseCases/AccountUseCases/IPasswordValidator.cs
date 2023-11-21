namespace Domain.UseCases.AccountUseCases
{
    public interface IPasswordValidator
    {
        bool Validate(string value, string storedValue);
    }
}
