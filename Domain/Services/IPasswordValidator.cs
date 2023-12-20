namespace Domain.Services
{
    public interface IPasswordValidator
    {
        bool Validate(string value, string storedValue);
    }
}
