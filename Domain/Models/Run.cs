namespace Domain.Models
{
    public class Run : EntityBase
    {
        public Route Route { get; set; } 
        public DateTime Departure { get; set; }
        public DateTime EstimatedArrival { get; set; }
        public Vehicle Bus { get; set; }
        public ICollection<Driver> Drivers { get; set; } = new HashSet<Driver>();
    }
}
