namespace Domain.Models
{
    public class Passport : EntityBase
    {
        public string Number { get; set; }
        public string Series { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
