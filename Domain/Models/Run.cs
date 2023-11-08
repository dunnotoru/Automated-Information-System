﻿using Domain.Models.Drivers;

namespace Domain.Models
{
    public class Run : DomainObject
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

        public Route RunRoute { get; }
        public DateTime Departure { get; }
        public DateTime ExtimatedArrival { get; }
        public Vehicle Bus { get; }
        public List<Driver> Drivers { get; }
    }
}
