using System;
using System.Linq;
using InformationSystem.Domain.Models;
using InformationSystem.Services.Abstractions;

namespace InformationSystem.Services;

public class ArrivalTimeCalculator : IArrivalTimeCalculator
{
    public DateTime Calculate(Route route, DateTime departureDateTime) 
        => route.Stations.Aggregate(departureDateTime, (current, _) => current.AddMinutes(30));
}