namespace Domain.Core.Drivers
{
    internal class DriverLicense
    {
        public DateOnly IssueDate { get; }
        public DateOnly ExpirationDate { get; }
        public Dictionary<string, DateOnly> Categories { get; }
        public string LicenseId { get; }
    }
}
