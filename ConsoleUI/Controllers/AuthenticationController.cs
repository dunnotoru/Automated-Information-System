using Domain.Services.Authentication;

namespace ConsoleUI.Controllers
{
    public class AuthenticationController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<bool> Authenticate(string name, string password)
        {
            try
            {
                await _authenticationService.AuthenticateAsync(name, password);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
