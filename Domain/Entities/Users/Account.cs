namespace Domain.Entities.Users
{
    public class Account
    {
        public Account(Permission accountPermissions, string name, string passwordHash)
        {
            AccountPermissions = accountPermissions;
            Name = name;
            PasswordHash = passwordHash;
        }

        public Permission AccountPermissions { get; }
        public string Name { get; }
        public string PasswordHash { get; }
    }
}
