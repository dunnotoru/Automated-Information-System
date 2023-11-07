namespace Domain.Entities.Drivers
{
    public class DriverLicense
    {
        public DriverLicense(DateOnly dateOfIssue, DateOnly dateOfExpitarion,
            Dictionary<string, DateOnly> categories, 
            string licenseId)
        {
            DateOfIssue = dateOfIssue;
            DateOfExpitarion = dateOfExpitarion;
            Categories = categories;
            LicenseId = licenseId;
        }

        public DateOnly DateOfIssue { get; }
        public DateOnly DateOfExpitarion { get; }
        public Dictionary<string, DateOnly> Categories { get; }
        public string LicenseId { get; }
    }
}
