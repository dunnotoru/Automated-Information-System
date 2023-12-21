namespace Domain.Models
{
    public class Schedule : EntityBase
    {
        public int PeriodInMinutes { get; set; }
        public ICollection<Run> Run { get; set; } = new List<Run>();


        public int RouteId { get; set; }
        public Route Route { get; set; }
    }
}
