namespace Domain.Entities.Drivers
{
    public class Driver
    {
        public Driver(string payrollNumber, string name,
            string surname, string patronymic, 
            DateOnly dateOfBirth, DriverLicense license, 
            string driverClass, string professionalStandartDetails, 
            string employmentBookDetails)
        {
            PayrollNumber = payrollNumber;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            DateOfBirth = dateOfBirth;
            License = license;
            DriverClass = driverClass;
            ProfessionalStandartDetails = professionalStandartDetails;
            EmploymentBookDetails = employmentBookDetails;
        }

        public string PayrollNumber { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Patronymic { get; }
        public DateOnly DateOfBirth { get; }
        public DriverLicense License { get; }
        public string DriverClass { get; } 
        public string ProfessionalStandartDetails { get; }
        public string EmploymentBookDetails { get; }

    }
}
