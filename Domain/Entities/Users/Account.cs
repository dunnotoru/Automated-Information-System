namespace Domain.Entities.Users
{
    public class Account
    {
        public Account(Permission accountPermissions, int id, 
            string name, string passwordHash)
        {
            AccountPermissions = accountPermissions;
            Id = id;
            Name = name;
            PasswordHash = passwordHash;
        }

        public Permission AccountPermissions { get; }
        public int Id { get; }
        public string Name { get; }
        public string PasswordHash { get; }
    }
}
