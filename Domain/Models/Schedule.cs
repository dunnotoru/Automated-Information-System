namespace Domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public ICollection<Run> Run { get; set; }
        public int PeriodInHours { get; set; }

        public Schedule()
        {
            Run = new HashSet<Run>();
        }
    }
}
