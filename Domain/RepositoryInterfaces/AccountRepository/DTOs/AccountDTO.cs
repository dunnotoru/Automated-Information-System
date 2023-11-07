using Domain.Entities.Users;

namespace Domain.RepositoryInterfaces.AccountRepository.DTOs
{
    public class AccountDTO
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public Permission Permissions { get; set; }
    }
}
