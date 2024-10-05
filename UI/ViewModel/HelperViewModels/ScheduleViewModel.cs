using Domain.Models;
using System;

namespace UI.ViewModel.HelperViewModels;

internal class ScheduleViewModel : ViewModelBase
{
    private int _id;
    private string _routeName;
    private string _runNumber;
    private string _driverFullName;
    private string _vehicleLicensePlate;
    private DateTime _departureDateTime;
    private DateTime _arrivalDateTime;

    public ScheduleViewModel(Schedule schedule)
    {
        Id = schedule.Id;
        RouteName = schedule.Route.Name;
        RunNumber = schedule.Run.Number;
        string name = schedule.Run.Driver.Name ?? "";
        string surname = schedule.Run.Driver.Surname ?? "";
        string patronymic = schedule.Run.Driver.Patronymic ?? "";
        VehicleLicensePlate = schedule.Run.Vehicle.LicensePlateNumber;
        DriverFullName = $"{surname} {name} {patronymic}";
        DepartureDateTime = schedule.Run.DepartureDateTime;
        ArrivalDateTime = schedule.Run.EstimatedArrivalDateTime;
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; NotifyPropertyChanged(); }
    }
    public string RouteName
    {
        get { return _routeName; }
        set { _routeName = value; NotifyPropertyChanged(); }
    }
    public string RunNumber
    {
        get { return _runNumber; }
        set { _runNumber = value; NotifyPropertyChanged(); }
    }
    public string DriverFullName
    {
        get { return _driverFullName; }
        set { _driverFullName = value; NotifyPropertyChanged(); }
    }
    public string VehicleLicensePlate
    {
        get { return _vehicleLicensePlate; }
        set { _vehicleLicensePlate = value; NotifyPropertyChanged(); }
    }
        
    public DateTime DepartureDateTime
    {
        get { return _departureDateTime; }
        set { _departureDateTime = value; NotifyPropertyChanged(); }
    }
    public DateTime ArrivalDateTime
    {
        get { return _arrivalDateTime; }
        set { _arrivalDateTime = value; NotifyPropertyChanged(); }
    }
}