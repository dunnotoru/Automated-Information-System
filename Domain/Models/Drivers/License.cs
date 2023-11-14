namespace Domain.Models.Drivers
{
    public class License
    {
        public string LicenseId { get; set; }
        public DateTime DateOfIssue { get; set; }
        public DateTime DateOfExpitarion { get; set; }
        public ICollection<Category> Caterories { get; set;}
    }
}
