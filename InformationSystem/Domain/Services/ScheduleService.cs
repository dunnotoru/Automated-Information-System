using System;
using System.Collections.Generic;
using System.Linq;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.RepositoryInterfaces;

namespace InformationSystem.Domain.Services;

public class ScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IRunRepository _runRepository;

    public ScheduleService(IScheduleRepository scheduleRepository, IRunRepository runRepository)
    {
        _scheduleRepository = scheduleRepository;
        _runRepository = runRepository;
    }

    public void UpdateSchedule()
    {
        try
        {
            List<Schedule> schedules = _scheduleRepository.GetAll().ToList();
            foreach (Run item in _runRepository.GetAll())
            {
                if (item.DepartureDateTime > DateTime.Now)
                {
                    int period = _scheduleRepository.GetByRun(item).PeriodInMinutes;
                    item.DepartureDateTime.AddMinutes(period);
                    item.EstimatedArrivalDateTime.AddMinutes(period);
                }

                _runRepository.Update(item.Id, item);
            }
        }
        catch
        {
                
        }
    }
}