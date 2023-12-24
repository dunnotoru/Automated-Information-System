using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;
using UI.ViewModel.HelperViewModels;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    internal class RunEditViewModel : ViewModelBase
    {
        private readonly IRunRepository _runRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IScheduleRepository _scheduleRepository;

        private int _id;
        private string _number;
        private RouteViewModel _selectedRoute;
        private DateTime _departureDateTime;
        private DateTime _estimatedArrivalDateTime;
        private int _periodity;
        private VehicleViewModel _selectedVehicle;
        private ObservableCollection<RouteViewModel> _routes;
        private ObservableCollection<VehicleViewModel> _vehicles;
        private ObservableCollection<DriverViewModel> _drivers;
        private ObservableCollection<DriverViewModel> _selectedDrivers;

        public Action<RunEditViewModel> RemoveEvent;
        public Action<string> ErrorEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public RunEditViewModel(Run run, IRunRepository runRepository, IRouteRepository routeRepository,
            IVehicleRepository vehicleRepository, IDriverRepository driverRepository, IScheduleRepository scheduleRepository) : this()
        {
            _runRepository = runRepository;
            _routeRepository = routeRepository;
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;

            Id = run.Id;
            Number = run.Number;
            SelectedRoute = new RouteViewModel(run.Route, _routeRepository);
            DepartureDateTime = run.DepartureDateTime;
            EstimatedArrivalDateTime = run.EstimatedArrivalDateTime;
            SelectedVehicle = new VehicleViewModel(run.Vehicle, _vehicleRepository);
            SelectedDrivers = new ObservableCollection<DriverViewModel>();
            foreach (Driver item in run.Drivers)
            {
                DriverViewModel vm = new DriverViewModel(item);
                SelectedDrivers.Add(vm);
            }


            Drivers = new ObservableCollection<DriverViewModel>();
            Routes = new ObservableCollection<RouteViewModel>();
            Vehicles = new ObservableCollection<VehicleViewModel>();

            foreach (var item in _driverRepository.GetAll())
            {
                DriverViewModel vm = new DriverViewModel(item);
                Drivers.Add(vm);
            }
            foreach (var item in _routeRepository.GetAll())
            {
                RouteViewModel vm = new RouteViewModel(item, _routeRepository);
                Routes.Add(vm);
            }
            foreach (var item in _vehicleRepository.GetAll())
            {
                VehicleViewModel vm = new VehicleViewModel(item, _vehicleRepository);
                Vehicles.Add(vm);
            }
            _scheduleRepository = scheduleRepository;
        }

        public RunEditViewModel(IRunRepository runRepository, IRouteRepository routeRepository,
            IVehicleRepository vehicleRepository, IDriverRepository driverRepository, IScheduleRepository scheduleRepository) : this()
        {
            _runRepository = runRepository;
            _routeRepository = routeRepository;
            _vehicleRepository = vehicleRepository;
            _driverRepository = driverRepository;

            Id = 0;
            Number = "";
            SelectedRoute = new RouteViewModel(_routeRepository);
            DepartureDateTime = DateTime.Now;
            EstimatedArrivalDateTime = DateTime.Now;
            SelectedVehicle = new VehicleViewModel(_vehicleRepository);
            SelectedDrivers = new ObservableCollection<DriverViewModel>();


            Drivers = new ObservableCollection<DriverViewModel>();
            Routes = new ObservableCollection<RouteViewModel>();
            Vehicles = new ObservableCollection<VehicleViewModel>();
            foreach (var item in _driverRepository.GetAll())
            {
                DriverViewModel vm = new DriverViewModel(item);
                Drivers.Add(vm);
            }
            foreach (var item in _routeRepository.GetAll())
            {
                RouteViewModel vm = new RouteViewModel(item, _routeRepository);
                Routes.Add(vm);
            }
            foreach (var item in _vehicleRepository.GetAll())
            {
                VehicleViewModel vm = new VehicleViewModel(item, _vehicleRepository);
                Vehicles.Add(vm);
            }
            _scheduleRepository = scheduleRepository;
        }

        private RunEditViewModel()
        {
            SaveCommand = new RelayCommand(Save, () => CanSave());
            RemoveCommand = new RelayCommand(Remove);
        }

        private bool CanSave()
        {
            return Vehicles.Count != 0 &&
                Drivers.Count != 0 && SelectedRoute != null &&
                SelectedVehicle != null && 
                Periodity > 0;
        }

        private void Save()
        {
            Run run = new Run()
            {
                Number = Number,
                Route = SelectedRoute.GetRoute(),
                DepartureDateTime = DepartureDateTime,
                EstimatedArrivalDateTime = DepartureDateTime.AddMinutes(SelectedRoute.GetRoute().Stations.Count * 30),
                Vehicle = SelectedVehicle.GetVehicle(),
            };
            
            try
            {
                if (Id == 0)
                {
                    _runRepository.Create(run);
                }
                else
                {
                    _runRepository.Update(Id, run);
                }
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }

            Schedule schedule = new Schedule()
            {
                Run = _runRepository.GetAll().First(o => o.Number == Number),
                PeriodInMinutes = Periodity,
            };

            try
            {
                if (Id == 0)
                {
                    _scheduleRepository.Create(schedule);
                }
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }

        private void Remove()
        {
            if (Id == 0) return;
            try
            {
                _runRepository.Remove(Id);

                RemoveEvent?.Invoke(this);
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
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
            set { _departureDateTime = value; OnPropertyChanged(); }
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

        public ObservableCollection<DriverViewModel> SelectedDrivers
        {
            get { return _selectedDrivers; }
            set { _selectedDrivers = value; OnPropertyChanged(); }
        }
    }
}
