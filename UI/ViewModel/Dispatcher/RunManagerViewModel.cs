using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel
{
    internal class RunManagerViewModel : ViewModelBase
    {
        private readonly IRunRepository _runRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IMessageBoxService _messageBoxService;

        private Run _selectedRun;
        private Route _selectedRoute;
        private Vehicle _selectedVehicle;
        private Driver _selectedDriver;

        private State _currentState;

        public ObservableCollection<Run> Runs { get; set; }
        public ObservableCollection<Route> Routes { get; set; }
        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public ObservableCollection<Driver> Drivers { get; set; }

        public Run SelectedRun
        {
            get => _selectedRun;
            set { _selectedRun = value; NotifyPropertyChangedByCallerName(); }
        }
        public Route SelectedRoute
        {
            get => _selectedRoute;
            set { _selectedRoute = value; NotifyPropertyChangedByCallerName(); }
        }
        public Vehicle SelectedVehicle
        {
            get => _selectedVehicle;
            set { _selectedVehicle = value; NotifyPropertyChangedByCallerName(); }
        }
        public Driver SelectedDriver
        {
            get => _selectedDriver;
            set { _selectedDriver = value; NotifyPropertyChangedByCallerName(); }
        }

        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                NotifyPropertyChanged(nameof(IsRedactingEnabled));
            }
        }

        public bool IsRedactingEnabled => CurrentState != State.None;

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DenyCommand { get; }

        public RunManagerViewModel(IRunRepository runRepository, IRouteRepository routeRepository,
            IVehicleRepository vehicleRepository, IMessageBoxService messageBoxService, IDriverRepository driverRepository)
        {
            ArgumentNullException.ThrowIfNull(runRepository);
            ArgumentNullException.ThrowIfNull(routeRepository);
            ArgumentNullException.ThrowIfNull(vehicleRepository);

            _runRepository = runRepository;
            _routeRepository = routeRepository;
            _vehicleRepository = vehicleRepository;
            _messageBoxService = messageBoxService;
            _driverRepository = driverRepository;

            try
            {
                Runs = new ObservableCollection<Run>(_runRepository.GetAll());
                Routes = new ObservableCollection<Route>(_routeRepository.GetAll());
                Vehicles = new ObservableCollection<Vehicle>(_vehicleRepository.GetAll());
                Drivers = new ObservableCollection<Driver>(_driverRepository.GetAll());
            }
            catch (DbUpdateException)
            {
                _messageBoxService.ShowMessage("Ошибка. Попробуйте перезапустить страницу");
            }

            AddCommand = new RelayCommand(Add, () => CurrentState == State.None);
            DeleteCommand = new RelayCommand(Delete, () => CurrentState == State.None && SelectedRun != null);
            EditCommand = new RelayCommand(Edit, () => CurrentState == State.None && SelectedRun != null);
            SaveCommand = new RelayCommand(Save, () => CurrentState != State.None);
            DenyCommand = new RelayCommand(Deny, () => CurrentState != State.None);
        }

        private void Add()
        {
            CurrentState = State.Add;
            SelectedRun = new Run();
        }

        private void Delete()
        {
           if (SelectedRun == null) return;
            try
            {
               _runRepository.Remove(SelectedRun);
               Runs.Remove(SelectedRun);
            }
            catch (DbUpdateException e)
            {
               _messageBoxService.ShowMessage(e.Message);
            }
        }

        private void Edit()
        {
            CurrentState = State.Edit;
        }

        private void Save()
        {
            if (SelectedRun == null)
            {
                _messageBoxService.ShowMessage("Не выбран рейс");
                return;
            }
            if (SelectedRoute == null)
            {
                _messageBoxService.ShowMessage("Не выбран маршрут");
                return; 
            }

            SelectedRun.Drivers.Add(SelectedDriver);
            SelectedRun.Route = SelectedRoute;
            SelectedRun.Vehicle = SelectedVehicle;

            try
            {
                if (CurrentState == State.Add)
                {
                    _runRepository.Add(SelectedRun);
                    Runs.Add(SelectedRun);
                }
                else if (CurrentState == State.Edit)
                {
                    _runRepository.Update(SelectedRun);
                }
            }
            catch(DbUpdateException e)
            {
                _messageBoxService.ShowMessage(e.Message);
            }

            CurrentState = State.None;
        }

        private void Deny()
        {
            CurrentState = State.None;
        }
    }
}
