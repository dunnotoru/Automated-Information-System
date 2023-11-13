namespace Domain.EntityFramework.Entities
{
    public class DriverEntity
    {
        public int PayrollNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string BirthDate { get; set;}
        public LicenseEntity License { get; set; }
        public string DriverClass { get; set; }
        public string ProfessionalStandartDetails { get; set; }
        public string EmploymentBookDetails { get; set; }
    }
}