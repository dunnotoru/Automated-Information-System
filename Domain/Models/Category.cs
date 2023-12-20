namespace Domain.Models
{
    public class Category : EntityBase
    {
        public string Name { get; set; }
        public ICollection<DriverLicense> Licenses { get; set;}
    }
}