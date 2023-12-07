namespace Domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public Route Route { get; set; }
        public int PeriodInMinutes { get; set; }
        public ICollection<Run> Run { get; set; }

        public Schedule()
        {
            Run = new HashSet<Run>();
        }
    }
}
