namespace Domain.Exceptions
{
    public class UsernameAlreadyExistException : RegisterException
    {

        public UsernameAlreadyExistException(string? message) : base(message) { }
    }
}