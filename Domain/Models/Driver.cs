﻿namespace Domain.Models
{
    public class Driver : EntityBase
    {
        public string PayrollNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string DriverClass { get; set; }
        public string ProfessionalStandardDetails { get; set; }
        public string EmploymentBookDetails { get; set; }
        

        public int? RunId { get; set; }
        public int DriverLicenseId { get; set; }
        public Run? Run { get; set; }
        public DriverLicense DriverLicense { get; set; }
    }
}
