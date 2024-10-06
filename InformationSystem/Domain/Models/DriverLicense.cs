namespace InformationSystem.Domain.Models;

public class DriverLicense : EntityBase
{
    public string LicenseNumber { get; set; }
    public DateTime DateOfIssue { get; set; }
    public DateTime DateOfExpiration { get; set; }
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public Driver Driver { get; set; }
    public int DriverId { get; set; }
}