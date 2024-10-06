using System.Security.Cryptography;
using System.Text;
using InformationSystem.Services.Abstractions;

namespace InformationSystem.Services;

internal class PasswordHasher : IPasswordHasher
{
    public string CalcHash(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}