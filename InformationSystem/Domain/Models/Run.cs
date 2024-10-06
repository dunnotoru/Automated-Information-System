namespace InformationSystem.Domain.Models;

public class Run : EntityBase
{
    public string Number { get; set; }
    public DateTime DepartureDateTime { get; set; }
    public DateTime EstimatedArrivalDateTime { get; set; }

    public int RouteId { get; set; }
    public int VehicleId { get; set; }
    public int? ScheduleId { get; set; }
    public int DriverId { get; set; }
    public Route Route { get; set; }
    public Vehicle Vehicle { get; set; }
    public Schedule? Schedule { get; set; }
    public Driver Driver { get; set; }
}