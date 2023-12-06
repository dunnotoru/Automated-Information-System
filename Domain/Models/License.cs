namespace Domain.Models.Drivers
{
    public class License
    {
        public string LicenseId { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfExpiration { get; set; }
        public ICollection<Category> Categories { get; set;}
    }
}
