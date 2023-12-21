namespace Domain.Models
{
    public class LicenseCategory
    {
        public DriverLicense DriverLicense { get; set; }
        public Category Category { get; set; }
        public int DriverLicenseId { get; set; }
        public int CategoryId { get; set; }
    }
}
