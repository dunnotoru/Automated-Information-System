using System;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.Services.Abstractions;

namespace InformationSystem.Services;

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