namespace Domain.Models.Drivers
{
    public class Driver
    {
        public Driver(string payrollNumber, string name,
            string surname, string patronymic, 
            DateOnly dateOfBirth, License license, 
            string driverClass, string professionalStandartDetails, 
            string employmentBookDetails)
        {
            PayrollNumber = payrollNumber;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            BirthDate = dateOfBirth;
            License = license;
            DriverClass = driverClass;
            ProfessionalStandartDetails = professionalStandartDetails;
            EmploymentBookDetails = employmentBookDetails;
        }

        public string PayrollNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set;}
        public string Patronymic { get; set; }
        public DateOnly BirthDate { get; set; }
        public License License { get; set; }
        public string DriverClass { get; set; } 
        public string ProfessionalStandartDetails { get; set; }
        public string EmploymentBookDetails { get; set; }

    }
}
