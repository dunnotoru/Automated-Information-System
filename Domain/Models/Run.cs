namespace Domain.Models
{
    public class Run
    {
        public int Number { get; set; }
        public Route Route { get; set; }
        public DateTime Departure { get; set; }
        public DateTime EstimatedArrival { get; set; }
        public Vehicle Bus { get; set; }
        public ICollection<Driver> Drivers { get; set; }
    }
}
