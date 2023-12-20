using Domain.Models;

namespace Domain.EntityFramework.Configurations
{
    public class LicenseCategory
    {
        public DriverLicense DriverLicense { get; set; }
        public Category Category { get; set; }
    }
}
