namespace Domain.UseCases.AccountUseCases
{
    public interface IPasswordHasher
    {
        string CalcHash(string password);
    }
}
