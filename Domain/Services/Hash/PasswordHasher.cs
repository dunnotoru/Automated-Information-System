namespace Domain.Services.Hash
{
    public class PasswordHasher : IPasswordHasher
    {
        public Task<string> HashPasswordAsync(string password)
        {
            return Task.FromResult(password);
        }
    }
}
