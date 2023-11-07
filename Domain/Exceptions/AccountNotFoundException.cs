﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class AccountNotFoundException : AuthenticationException
    {
        public AccountNotFoundException(string? message) : base(message)
        {

        }
    }
}
