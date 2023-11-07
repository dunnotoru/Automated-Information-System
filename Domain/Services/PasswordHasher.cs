namespace Domain.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public Task<string> HashPasswordAsync(string password)
        {
            return Task.FromResult(password);
        }
    }
}
