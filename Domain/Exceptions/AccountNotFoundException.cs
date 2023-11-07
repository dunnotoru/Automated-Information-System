namespace Domain.Exceptions
{
    public class AccountNotFoundException : AuthenticationException
    {
        public AccountNotFoundException(string? message) : base(message) { }
    }
}
