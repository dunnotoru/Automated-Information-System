namespace Domain.Models
{
    public class Schedule : EntityBase
    {
        public Route Route { get; set; }
        public int PeriodInMinutes { get; set; }
        public ICollection<Run> Run { get; set; }

        public Schedule()
        {
            Run = new HashSet<Run>();
        }
    }
}
