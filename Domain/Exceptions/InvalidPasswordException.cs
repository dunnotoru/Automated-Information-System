namespace Domain.Exceptions
{
    internal class InvalidPasswordException : AuthenticationException
    {
        public InvalidPasswordException(string? message) : base(message)
        {

        }
    }
}
