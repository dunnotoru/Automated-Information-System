using System;
using System.Collections.Generic;
using InformationSystem.Domain.Models;

namespace InformationSystem.Services;

public class RunSearchService
{
    public RunSearchService()
    {
        
    }

    public List<Run> GetAvailableRuns(Station departureStation, Station arrivalStation, DateTime departureDateTimeMinimum,
        DateTime departureDateTimeMaximum)
    {
        throw new NotImplementedException();
    }
}