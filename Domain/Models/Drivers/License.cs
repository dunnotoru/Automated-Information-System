namespace Domain.Models.Drivers
{
    public class License
    {
        public License(string licenseId, DateOnly dateOfIssue, 
            DateOnly dateOfExpitarion, 
            Dictionary<string, DateOnly> categories)
        {
            LicenseId = licenseId;
            DateOfIssue = dateOfIssue;
            DateOfExpitarion = dateOfExpitarion;
            Categories = categories;
        }

        public string LicenseId { get; }
        public DateOnly DateOfIssue { get; }
        public DateOnly DateOfExpitarion { get; }
        public Dictionary<string, DateOnly> Categories { get; }
    }
}
