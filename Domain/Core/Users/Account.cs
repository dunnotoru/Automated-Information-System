namespace Domain.Core.Users
{
    internal class Account
    {
        public Permission AccountPermissions { get; }

        public string Name { get; }
        public string PasswordHash { get; }
    }
}
