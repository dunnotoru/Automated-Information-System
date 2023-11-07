﻿using Domain.RepositoryInterfaces.AccountRepository;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EntityFramework.Entities
{
    public class AccountEntity
    {
        [Key]
        public int Id { get; set; }
        [Column("name")]
        public string? Name { get; set; }
        [Column("password_hash")]
        public string? PasswordHash { get; set; }
        [Column("read")]
        public bool Read { get; set; }
        [Column("write")]
        public bool Write { get; set; }
        [Column("edit")]
        public bool Edit { get; set; }
        [Column("delete")]
        public bool Delete { get; set; }

        public AccountEntity() { }
        public AccountEntity(AddAccountDTO dto)
        {
            Name = dto.Name;
            PasswordHash = dto.PasswordHash;
            Read = dto.Permissions.Read;
            Write = dto.Permissions.Write;
            Edit = dto.Permissions.Edit;
            Delete = dto.Permissions.Delete;
        }
    }
}
