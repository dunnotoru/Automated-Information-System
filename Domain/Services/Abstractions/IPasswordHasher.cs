namespace Domain.Services.Abstractions;

public interface IPasswordHasher
{
    string CalcHash(string password);
}