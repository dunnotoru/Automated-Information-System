using Domain.Models.Users;
using Domain.EntityFramework.Entities;

namespace Domain.EntityFramework.Mappers
{
    public static class AccountMapper
    {
        public static Account ToDomain(AccountEntity entity)
        {
            Permission p = new Permission(
                entity.Read,
                entity.Write,
                entity.Edit,
                entity.Delete);

            Account model = new Account(
                entity.Id,
                entity.Name,
                entity.PasswordHash,
                p);

            return model;
        }

        public static AccountEntity ToEntity(Account model)
        {
            AccountEntity entity = new AccountEntity();
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.PasswordHash = model.PasswordHash;
            entity.Read = model.AccountPermissions.Read;
            entity.Write = model.AccountPermissions.Write;
            entity.Edit = model.AccountPermissions.Edit;
            entity.Delete = model.AccountPermissions.Delete;
            return entity;
        }
    }
}
