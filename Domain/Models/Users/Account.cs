namespace Domain.Models.Users
{
    public class Account
    {
        public Account(int id, string name, string passwordHash, Permission accountPermissions)
        {
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
            AccountPermissions = accountPermissions;
        }

        public int Id { get; }
        public string Name { get; }
        public string PasswordHash { get; }
        public Permission AccountPermissions { get; }
    }
}
