﻿using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface IPassportRepository : IRepositoryBase<IdentityDocument>
    {
        IdentityDocument? Get(string number, string series);
        IdentityDocument? GetById(int id);
    }
}