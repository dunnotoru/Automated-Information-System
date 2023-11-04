namespace Domain.Core.Drivers
{
    internal class Driver
    {
        public string PayrollNumber { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Patronymic { get; }
        public DateOnly BirthDate { get; }
        public DriverLicense License { get; }
        public string DriverClass { get; } // М, 0, 1 ... 13
        public string ProfessionalStandartDetails { get; }
        public string EmploymentBookDetails { get; }

    }
}
