namespace Domain.Services
{
    public interface IPasswordHasher
    {
        string CalcHash(string password);
    }
}
