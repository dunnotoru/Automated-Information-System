﻿namespace Domain.Exceptions
{
    public class RegisterException : Exception
    {
        public RegisterException(string? message) : base(message) { }
    }
}
