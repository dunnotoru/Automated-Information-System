using Domain.Models;
using Domain.Services;
using System;

namespace UI.Services
{
    public class ArrivalTimeCalculator : IArrivalTimeCalculator
    {
        public DateTime Calculate(Route route, DateTime departureDateTime)
        {
            foreach (var item in route.Stations)
            {
                departureDateTime = departureDateTime.AddMinutes(30);
            }
            return departureDateTime;
        }
    }
}
