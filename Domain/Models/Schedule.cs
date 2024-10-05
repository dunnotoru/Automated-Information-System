namespace Domain.Models;

public class Schedule : EntityBase
{
    public int PeriodInMinutes { get; set; }

    public int RouteId { get; set; }
    public Route Route { get; set; }

    public int RunId { get; set; }
    public Run Run { get; set; }
}