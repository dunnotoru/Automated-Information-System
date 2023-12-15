using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    internal class RouteManagerViewModel : ViewModelBase
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IStationRepository _stationRepository;

        private Route _selectedRoute;
        private Station _selectedStation;
        private Station _routeSelectedStation;
        private State _currentState;

        private ObservableCollection<Station> _selectedRouteStations;
        private ObservableCollection<Station> _availableStations;

        public ObservableCollection<Route> Routes { get; set; }
        public List<Station> Stations { get; set; }
        public ObservableCollection<Station> AvailableStations
        {
            get => _availableStations;
            set { _availableStations = value; OnPropertyChangedByCallerName(); }
        }
        public ObservableCollection<Station> SelectedRouteStations
        {
            get => _selectedRouteStations;
            set { _selectedRouteStations = value; OnPropertyChangedByCallerName(); }
        }
        public Route SelectedRoute
        {
            get => _selectedRoute;
            set 
            { 
                _selectedRoute = value;
                if(_selectedRoute == null)
                {
                    AvailableStations = new ObservableCollection<Station>();
                    SelectedRouteStations = new ObservableCollection<Station>();
                }
                else
                {
                    SelectedRouteStations = new ObservableCollection<Station>(_selectedRoute.Stations);
                    AvailableStations = new ObservableCollection<Station>(Stations.Where(x => !SelectedRouteStations.Any(_ => _.Id == x.Id)));
                }
                OnPropertyChangedByCallerName(); 
            }
        }
        
        public Station SelectedStation
        {
            get => _selectedStation;
            set { _selectedStation = value; OnPropertyChangedByCallerName(); }
        }
        public Station RouteSelectedStation
        {
            get => _routeSelectedStation;
            set { _routeSelectedStation = value; OnPropertyChangedByCallerName(); }
        }
        public State CurrentState
        {
            get => _currentState;
            set 
            { 
                _currentState = value;
                OnPropertyChanged(nameof(IsRedactingEnabled));
            }
        }
        public bool IsRedactingEnabled => CurrentState != State.None;

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DenyCommand { get; }

        public ICommand MoveToRouteCommand { get; }
        public ICommand RemoveFromRouteCommand { get; }
        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }

        public RouteManagerViewModel(IRouteRepository routeRepository, IStationRepository stationRepository)
        {
            ArgumentNullException.ThrowIfNull(routeRepository);
            ArgumentNullException.ThrowIfNull(stationRepository);
            _routeRepository = routeRepository;
            _stationRepository = stationRepository;

            Routes = new ObservableCollection<Route>(_routeRepository.GetAll());
            Stations = new List<Station>(_stationRepository.GetAll());

            AddCommand = new RelayCommand(Add, () => CurrentState == State.None);
            DeleteCommand = new RelayCommand(Delete, () => CurrentState == State.None && SelectedRoute != null);
            EditCommand = new RelayCommand(Edit, () => CurrentState == State.None && SelectedRoute != null);
            SaveCommand = new RelayCommand(Save, () => CurrentState != State.None);
            DenyCommand = new RelayCommand(Deny, () => CurrentState != State.None);
            MoveToRouteCommand = new RelayCommand(MoveToRoute, () => CurrentState != State.None);
            RemoveFromRouteCommand = new RelayCommand(RemoveFromRoute, () => CurrentState != State.None);
            MoveUpCommand = new RelayCommand(MoveUp, () => CurrentState != State.None);
            MoveDownCommand = new RelayCommand(MoveDown, () => CurrentState != State.None);
        }

        private void Add()
        {
            SelectedRoute = new Route();
            CurrentState = State.Add;
        }

        private void Delete()
        {
            if(SelectedRoute == null) return;
            _routeRepository.Remove(SelectedRoute.Id);
            Routes.Remove(SelectedRoute);
        }

        private void Edit()
        {
            CurrentState = State.Edit;
        }

        private void Save()
        {
            SelectedRoute.Stations.Clear();
            foreach (var s in SelectedRouteStations)
            {
                SelectedRoute.Stations.Add(new Station()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Address = s.Address,
                });
            }

            if (CurrentState == State.Add)
            {
                _routeRepository.Add(SelectedRoute);
                Routes.Add(SelectedRoute);
            }
            else if (CurrentState == State.Edit)
            {
                _routeRepository.Update(SelectedRoute.Id, SelectedRoute);
            }
            CurrentState = State.None;
        }

        private void Deny()
        {
            CurrentState = State.None;
        }

        private void MoveToRoute()
        {
            if(SelectedRoute == null) return;
            if(SelectedStation == null) return;
            SelectedRouteStations.Add(SelectedStation);
            AvailableStations.Remove(SelectedStation);
        }
        private void RemoveFromRoute()
        {
            if (SelectedRoute == null) return;
            if (RouteSelectedStation == null) return;
            AvailableStations.Add(RouteSelectedStation);
            SelectedRouteStations.Remove(RouteSelectedStation);
        }
        private void MoveUp()
        {
            if (SelectedRoute == null) return;
            if (RouteSelectedStation == null) return;
            int index = SelectedRouteStations.IndexOf(RouteSelectedStation);
            if (index == 0) return;
            SelectedRouteStations.Move(index, index - 1);
        }
        private void MoveDown()
        {
            if (SelectedRoute == null) return;
            if (RouteSelectedStation == null) return;
            int index = SelectedRouteStations.IndexOf(RouteSelectedStation);
            if (index == SelectedRouteStations.Count - 1) return;
            SelectedRouteStations.Move(index, index + 1);
        }
    }
}
