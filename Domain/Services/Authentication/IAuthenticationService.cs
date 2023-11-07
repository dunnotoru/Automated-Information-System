using Domain.Models.Users;

namespace Domain.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<Account> AuthenticateAsync(string name, string password);
    }
}
