namespace Domain.EntityFramework.Entities
{
    public class ScheduleLineEntity
    {
        public RunEntity Route { get; set; }
        public int Periodity { get; set; }
    }
}
