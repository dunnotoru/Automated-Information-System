using Domain.Services.AccountUseCases;

namespace Domain.Tests.AccountTests
{
    internal class PasswordHasherStub : IPasswordHasher
    {
        public string CalcHash(string password)
        {
            return password;
        }
    }
}
