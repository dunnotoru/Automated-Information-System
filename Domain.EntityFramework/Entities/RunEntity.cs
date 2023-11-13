namespace Domain.EntityFramework.Entities
{
    public class RunEntity
    {
        public int Number { get; set; }
        public RouteEntity Route { get; set; }
        public DateTime Departure {  get; set; }
        public DateTime Estimated { get; set; }
        public VehicleEntity Vehicle { get; set; }
        public ICollection<DriverEntity> Drivers { get; set; }
    }
}