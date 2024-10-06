using System;
using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.Services.Abstractions;

public interface IArrivalTimeCalculator
{
    DateTime Calculate(Route route, DateTime departureDateTime);
}