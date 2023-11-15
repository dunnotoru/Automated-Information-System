namespace Domain.Services.AccountUseCases
{
    public interface IPasswordHasher
    {
        string CalcHash(string password);
    }
}
