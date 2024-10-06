using System.Collections.Generic;

namespace InformationSystem.Domain.Models;

public class Route : EntityBase
{
    public string Name { get; set; }
    public ICollection<Station> Stations { get; set; } = new List<Station>();
    public ICollection<Run> Runs { get; set; } = new List<Run>();
    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
}