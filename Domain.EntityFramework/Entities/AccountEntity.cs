using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.EntityFramework.Entities
{
    public class AccountEntity
    {
        [Column("id")]
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
    }
}
