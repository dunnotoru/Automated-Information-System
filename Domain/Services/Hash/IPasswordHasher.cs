namespace Domain.Services.Hash
{
    public interface IPasswordHasher
    {
        Task<string> HashPasswordAsync(string password);
    }
}
