namespace Domain.Models
{
    public class License : EntityBase
    {
        public string LicenseNumber { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfExpiration { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
