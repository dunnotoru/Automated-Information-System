using Domain.UseCases.AccountUseCases;

namespace UI.Model
{
    internal class PasswordHasher : IPasswordHasher
    {
        public string CalcHash(string password)
        {
            return password + "_HASH";
        }
    }
}
