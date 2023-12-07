using Domain.Models;
using Domain.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public class RouteManagerViewModel : ViewModelBase
    {
        private readonly RouteService _routeService;
        private readonly StationService _stationService;

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
            set { _availableStations = value; NotifyPropertyChanged(nameof(AvailableStations)); }
        }
        public ObservableCollection<Station> SelectedRouteStations
        {
            get => _selectedRouteStations;
            set { _selectedRouteStations = value; NotifyPropertyChanged(nameof(SelectedRouteStations)); }
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
                NotifyPropertyChanged(nameof(SelectedRoute)); 
            }
        }
        public Station SelectedStation
        {
            get => _selectedStation;
            set { _selectedStation = value; NotifyPropertyChanged(nameof(SelectedStation)); }
        }
        public Station RouteSelectedStation
        {
            get => _routeSelectedStation;
            set { _routeSelectedStation = value; NotifyPropertyChanged(nameof(RouteSelectedStation)); }
        }
        public State CurrentState
        {
            get => _currentState;
            set 
            { 
                _currentState = value; 
                NotifyPropertyChanged(nameof(CurrentState));
                NotifyPropertyChanged(nameof(EnableSelection)); 
                NotifyPropertyChanged(nameof(EnableSettings)); 
            }
        }
        public bool EnableSelection => CurrentState == State.None;
        public bool EnableSettings => CurrentState != State.None;

        public ICommand AddCommand
            => new RelayCommand(Add, () => CurrentState == State.None);
        public ICommand DeleteCommand
            => new RelayCommand(Delete, () => CurrentState == State.None);
        public ICommand EditCommand
            => new RelayCommand(Edit, () => CurrentState == State.None);
        public ICommand SaveCommand 
            => new RelayCommand(Save, () => CurrentState != State.None);
        public ICommand DenyCommand
            => new RelayCommand(Deny, () => CurrentState != State.None);

        public ICommand MoveToRouteCommand
            => new RelayCommand(MoveToRoute, () => CurrentState != State.None);
        public ICommand RemoveFromRouteCommand
            => new RelayCommand(RemoveFromRoute, () => CurrentState != State.None);
        public ICommand MoveUpCommand
            => new RelayCommand(MoveUp, () => CurrentState != State.None);
        public ICommand MoveDownCommand
            => new RelayCommand(MoveDown, () => CurrentState != State.None);

        public RouteManagerViewModel(RouteService routeService, StationService stationService)
        {
            _routeService = routeService;
            _stationService = stationService;

            Routes = new ObservableCollection<Route>(_routeService.GetAll());
            Stations = new List<Station>(_stationService.GetAll());
        }

        private void Add()
        {
            SelectedRoute = new Route();
            CurrentState = State.Add;
        }

        private void Delete()
        {
            if(SelectedRoute == null) return;
            _routeService.Delete(SelectedRoute);
            Routes.Remove(SelectedRoute);
        }

        private void Edit()
        {
            CurrentState = State.Edit;

        }

        private void Save()
        {
            SelectedRoute.Stations = SelectedRouteStations;
            if (CurrentState == State.Add)
            {
                _routeService.Add(SelectedRoute);
                Routes.Add(SelectedRoute);
            }
            else if (CurrentState == State.Edit)
            {
                _routeService.Update(SelectedRoute);
            }
            CurrentState = State.None;
        }

        private void Deny()
        {
            CurrentState = State.None;
            SelectedRoute = null;
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
