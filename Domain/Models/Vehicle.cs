namespace Domain.Models
{
    public class Vehicle : EntityBase
    {
        public string LicensePlateNumber { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Capacity { get; set; }
        public DateTime Manufacture { get; set; }
        public DateTime LastRepair { get; set; }
        public string LastRepairType { get; set; }
        public int Mileage { get; set; }
        public string? Photography { get; set; }
        public string? Freighter { get; set; }
        public string InsuranceDetails { get; set; }
    }
}