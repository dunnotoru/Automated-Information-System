namespace Domain.Models
{
    public class Passport : DomainObject
    {
        public Passport(int id, int number, int series, 
            string name, string surname, 
            string patronymic, DateOnly dateOfBirth)
        {
            Id = id;
            Number = number;
            Series = series;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
        }

        public int Number { get; }
        public int Series { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Patronymic { get; }
        public DateOnly DateOfBirth { get; }
    }
}
