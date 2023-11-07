using Domain.Models.Users;

namespace Domain.Services.Registration
{
    public interface IRegistrationService
    {
        Task RegisterAsync(string name, string password, 
            string passwordConfrimation, Permission permission);
        Task RemoveAsync(int id);
    }
}
