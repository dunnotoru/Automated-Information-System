using Domain.UseCases.AccountUseCases;

namespace UI.Services
{
    internal class PasswordHasher : IPasswordHasher
    {
        public string CalcHash(string password)
        {
            return password + "_HASH";
        }
    }
}
