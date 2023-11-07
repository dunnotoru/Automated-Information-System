namespace Domain.Services
{
    public interface IPasswordHasher
    {
        Task<string> HashPasswordAsync(string password);
    }
}
