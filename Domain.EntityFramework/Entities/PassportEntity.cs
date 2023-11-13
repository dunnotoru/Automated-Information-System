namespace Domain.EntityFramework.Entities
{
    public class PassportEntity
    {
        public int Number { get; set; }
        public int Series { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}