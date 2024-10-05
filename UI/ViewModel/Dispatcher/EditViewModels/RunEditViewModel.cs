using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Domain.Services.Abstractions;
using UI.Command;
using UI.ViewModel.HelperViewModels;

namespace UI.ViewModel.Dispatcher.EditViewModels;

internal class RunEditViewModel : ViewModelBase
{
    private readonly IRunRepository _runRepository;
    private readonly IRouteRepository _routeRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IDriverRepository _driverRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IArrivalTimeCalculator _arrivalTimeCalculator;

    private int _id;
    private string _number;
    private RouteViewModel _selectedRoute;
    private DateTime _departureDateTime;
    private DateTime _estimatedArrivalDateTime;
    private string _departureTime;
    private int _periodity;
    private VehicleViewModel _selectedVehicle;
    private ObservableCollection<RouteViewModel> _routes;
    private ObservableCollection<VehicleViewModel> _vehicles;
    private ObservableCollection<DriverViewModel> _drivers;
    private DriverViewModel _selectedDriver;

    public event EventHandler Save;
    public event EventHandler Remove;
    public event EventHandler<Exception> Error;

    public ICommand SaveCommand { get; }
    public ICommand RemoveCommand { get; }

    public RunEditViewModel(Run run, IRunRepository runRepository, IRouteRepository routeRepository,
        IVehicleRepository vehicleRepository, IDriverRepository driverRepository, IScheduleRepository scheduleRepository, IArrivalTimeCalculator arrivalTimeCalculator)
        : this(runRepository, routeRepository, vehicleRepository, driverRepository, scheduleRepository, arrivalTimeCalculator)
    {
        Id = run.Id;
        Number = run.Number;
        SelectedRoute = new RouteViewModel(run.Route);
        DepartureDateTime = run.DepartureDateTime;

        DepartureTime = DepartureDateTime.ToShortTimeString();

        SelectedVehicle = new VehicleViewModel(_vehicleRepository.GetById(run.VehicleId));
        SelectedDriver = new DriverViewModel(_driverRepository.GetById(run.DriverId));

        Vehicles.Add(SelectedVehicle);
        Drivers.Add(SelectedDriver);
        try
        {
            Periodity = _scheduleRepository.GetByRun(run).PeriodInMinutes;
        }
        catch
        {
            Periodity = 0;
        }

    }

    public RunEditViewModel(IRunRepository runRepository, IRouteRepository routeRepository,
        IVehicleRepository vehicleRepository, IDriverRepository driverRepository, IScheduleRepository scheduleRepository, IArrivalTimeCalculator arrivalTimeCalculator)
    {
        _runRepository = runRepository;
        _routeRepository = routeRepository;
        _vehicleRepository = vehicleRepository;
        _driverRepository = driverRepository;
        _scheduleRepository = scheduleRepository;
        _arrivalTimeCalculator = arrivalTimeCalculator;

        Id = 0;
        Number = "";
        SelectedRoute = new RouteViewModel();
        DepartureDateTime = DateTime.Now;
        Periodity = 0;

        Drivers = new ObservableCollection<DriverViewModel>();
        Routes = new ObservableCollection<RouteViewModel>();
        Vehicles = new ObservableCollection<VehicleViewModel>();
        foreach (var item in _driverRepository.GetIdleDrivers())
        {
            DriverViewModel vm = new DriverViewModel(item);
            Drivers.Add(vm);
        }
        foreach (var item in _routeRepository.GetAll())
        {
            RouteViewModel vm = new RouteViewModel(item);
            Routes.Add(vm);
        }
        foreach (var item in _vehicleRepository.GetIdleVehicles())
        {
            VehicleViewModel vm = new VehicleViewModel(item);
            Vehicles.Add(vm);
        }

        SaveCommand = new RelayCommand(ExecuteSave, () => CanSave());
        RemoveCommand = new RelayCommand(ExecuteRemove);

        CalcEstimatedDateTime();
    }

    private bool CanSave()
    {
        return Vehicles.Count != 0 &&
               Drivers.Count != 0 && SelectedRoute != null &&
               SelectedVehicle != null && 
               Periodity > 0;
    }

    private void ExecuteSave()
    {
        TimeOnly t = TimeOnly.Parse(DepartureTime);

        DepartureDateTime = DepartureDateTime.Date;
        DepartureDateTime = DepartureDateTime.AddHours(t.Hour);
        DepartureDateTime = DepartureDateTime.AddMinutes(t.Minute);

        Run run = new Run()
        {
            Number = Number,
            Route = _routeRepository.GetById(SelectedRoute.Id),
            DepartureDateTime = DepartureDateTime,
            EstimatedArrivalDateTime = EstimatedArrivalDateTime,
            Vehicle = _vehicleRepository.GetById(SelectedVehicle.Id),
            Driver = _driverRepository.GetById(SelectedDriver.Id)
        };
            
        try
        {
            if (Id == 0)
            {
                Id = _runRepository.Create(run);
            }
            else
            {
                _runRepository.Update(Id, run);
            }
            Save?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Error?.Invoke(this, e);
        }

        Schedule schedule = new Schedule()
        {
            Run = _runRepository.GetAll().First(o => o.Number == Number),
            PeriodInMinutes = Periodity,
        };

        try
        {
            if (schedule.Id == 0)
            {
                _scheduleRepository.Create(schedule);
            }
        }
        catch (Exception e)
        {
            Error?.Invoke(this, e);
        }
    }

    private void ExecuteRemove()
    {
        if (Id == 0) return;
        try
        {
            _runRepository.Remove(Id);

            Remove?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Error?.Invoke(this, e);
        }
    }

    public ObservableCollection<RouteViewModel> Routes
    {
        get { return _routes; }
        set { _routes = value; OnPropertyChanged(); }
    }
    public ObservableCollection<VehicleViewModel> Vehicles
    {
        get { return _vehicles; }
        set { _vehicles = value; OnPropertyChanged(); }
    }
    public ObservableCollection<DriverViewModel> Drivers
    {
        get { return _drivers; }
        set { _drivers = value; OnPropertyChanged(); }
    }
    public int Id
    {
        get { return _id; }
        set { _id = value; OnPropertyChanged(); }
    }
    public string Number
    {
        get { return _number; }
        set { _number = value; OnPropertyChanged(); }
    }
    public RouteViewModel SelectedRoute
    {
        get { return _selectedRoute; }
        set { _selectedRoute = value; OnPropertyChanged(); }
    }
    public DateTime DepartureDateTime
    {
        get { return _departureDateTime; }
        set { _departureDateTime = value; OnPropertyChanged(); CalcEstimatedDateTime(); }
    }
    public int Periodity
    {
        get { return _periodity; }
        set { _periodity = value; OnPropertyChanged(); }
    }
    public DateTime EstimatedArrivalDateTime
    {
        get { return _estimatedArrivalDateTime; }
        set { _estimatedArrivalDateTime = value; OnPropertyChanged(); }
    }

    public VehicleViewModel SelectedVehicle
    {
        get { return _selectedVehicle; }
        set { _selectedVehicle = value; OnPropertyChanged(); }
    }
    public DriverViewModel SelectedDriver
    {
        get { return _selectedDriver; }
        set { _selectedDriver = value; OnPropertyChanged(); }
    }
        
    public string DepartureTime
    {
        get { return _departureTime; }
        set { _departureTime = value; OnPropertyChanged(); }
    }


    private void CalcEstimatedDateTime()
    {
        if (SelectedRoute == null || SelectedRoute.Id == 0) return;
        EstimatedArrivalDateTime = _arrivalTimeCalculator.Calculate(_routeRepository.GetById(SelectedRoute.Id), DepartureDateTime);
    }
}