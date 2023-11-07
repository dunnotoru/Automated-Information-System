namespace Domain.Exceptions
{
    public class OutOfPercentRangeException : ArgumentOutOfRangeException
    {
        public OutOfPercentRangeException(string? paramName) : base(paramName)
        {

        }
    }
}
