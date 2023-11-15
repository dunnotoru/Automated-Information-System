namespace Domain.Models
{
    public class Account
    {
        public int? Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
    }
}
