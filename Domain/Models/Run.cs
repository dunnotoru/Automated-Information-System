namespace Domain.Models
{
    public class Run : EntityBase
    {
        public string Number { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime EstimatedArrivalDateTime { get; set; }
        public ICollection<Driver> Drivers { get; set; } = new List<Driver>();


        public int RouteId { get; set; }
        public int VehicleId { get; set; }
        public Route Route { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
