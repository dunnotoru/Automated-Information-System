using Domain.Models.Drivers;

namespace Domain.Models
{
    public class Run
    {
        public Run(int number, Route runRoute,
            DateTime departure, DateTime extimatedArrival,
            Vehicle bus, List<Driver> drivers)
        {
            Number = number;
            RunRoute = runRoute;
            Departure = departure;
            ExtimatedArrival = extimatedArrival;
            Bus = bus;
            Drivers = drivers;
        }

        public int Number { get; }
        public Route RunRoute { get; }
        public DateTime Departure { get; }
        public DateTime ExtimatedArrival { get; }
        public Vehicle Bus { get; }
        public List<Driver> Drivers { get; }
    }
}
