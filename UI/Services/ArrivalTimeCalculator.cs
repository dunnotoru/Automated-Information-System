using Domain.Models;
using System;
using Domain.Services.Abstractions;

namespace UI.Services;

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