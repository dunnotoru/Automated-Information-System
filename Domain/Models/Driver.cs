namespace Domain.Models
{
    public class Driver
    {
        public string? PayrollNumber { get; set; }
        public License? License { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? DriverClass { get; set; }
        public string? ProfessionalStandartDetails { get; set; }
        public string? EmploymentBookDetails { get; set; }
    }
}
