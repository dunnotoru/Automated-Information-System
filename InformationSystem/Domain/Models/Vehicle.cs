namespace InformationSystem.Domain.Models;

public class Vehicle : EntityBase
{
    public string LicensePlateNumber { get; set; }
    public DateTime Manufacture { get; set; }
    public DateTime LastRepair { get; set; }
    public int Mileage { get; set; }
    public string? Photography { get; set; }
    public string InsuranceDetails { get; set; }
        

    public int FreighterId { get; set; }
    public int? RepairTypeId { get; set; }
    public int? RunId { get; set; }
    public int VehicleModelId { get; set; }
    public Freighter Freighter { get; set; }
    public RepairType? RepairType { get; set; }
    public Run? Run { get; set; }
    public VehicleModel VehicleModel { get; set; }
}