namespace Domain.EntityFramework.Entities
{
    public class LicenseEntity
    {
        public int Id { get; set; }
        public DateOnly DateOfIssue { get; set; }
        public DateOnly DateOfExpitarion { get; set; }
    }
}