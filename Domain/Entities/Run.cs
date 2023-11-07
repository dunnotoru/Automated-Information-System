using Domain.Entities.Drivers;

namespace Domain.Entities
{
    public class Run
    {
        public Run(int id, Route runRoute,
            DateTime departure, DateTime extimatedArrival,
            Vehicle bus, List<Driver> drivers)
        {
            Id = id;
            RunRoute = runRoute;
            Departure = departure;
            ExtimatedArrival = extimatedArrival;
            Bus = bus;
            Drivers = drivers;
        }

        public int Id { get; }
        public Route RunRoute { get; }
        public DateTime Departure { get; }
        public DateTime ExtimatedArrival { get; }
        public Vehicle Bus { get; }
        public List<Driver> Drivers { get; }
    }
}
