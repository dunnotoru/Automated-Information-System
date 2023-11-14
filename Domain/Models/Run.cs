﻿using Domain.Models.Drivers;

namespace Domain.Models
{
    public class Run
    {
        public int Number { get; set; }
        public Route RunRoute { get; set; }
        public DateTime Departure { get; set; }
        public DateTime ExtimatedArrival { get; set; }
        public Vehicle Bus { get; set; }
        public List<Driver> Drivers { get; set; }
    }
}
