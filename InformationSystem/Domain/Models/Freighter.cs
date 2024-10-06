namespace InformationSystem.Domain.Models;

public class Freighter : EntityBase
{
    public string Name { get; set; }
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}