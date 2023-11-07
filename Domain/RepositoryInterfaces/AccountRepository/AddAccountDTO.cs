using Domain.Models.Users;

namespace Domain.RepositoryInterfaces.AccountRepository
{
    public class AddAccountDTO
    {
        public AddAccountDTO(string name, string passwordHash, Permission permissions)
        {
            Name = name;
            PasswordHash = passwordHash;
            Permissions = permissions;
        }

        public string Name { get; }
        public string PasswordHash { get; }
        public Permission Permissions { get; }
    }
}
