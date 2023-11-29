﻿using Domain.UseCases.AccountUseCases;

namespace Domain.Tests.AccountTests
{
    internal class PasswordHasherStub : IPasswordHasher
    {
        public string CalcHash(string password)
        {
            return password;
        }
    }
}