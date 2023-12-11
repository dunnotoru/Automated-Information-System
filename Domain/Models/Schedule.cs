namespace Domain.Models
{
    public class Schedule : EntityBase
    {
        public Route Route { get; set; }
        public int PeriodInMinutes { get; set; }
        public ICollection<Run> Run { get; set; } = new HashSet<Run>();
    }
}
