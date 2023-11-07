using Domain.Models.Users;

namespace Domain.RepositoryInterfaces.AccountRepository
{
    public class AccountDTO
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public Permission Permissions { get; set; }
    }
}
