namespace Domain.Exceptions
{
    public class PasswordConfrimationDoesnotMatch : RegisterException
    {
        public PasswordConfrimationDoesnotMatch(string? message) : base(message) { }
    }
}
