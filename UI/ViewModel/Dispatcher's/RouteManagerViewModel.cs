using Domain.Models;
using Domain.Services;
using SQLitePCL;
using System.Collections.ObjectModel;
using System.Security.Authentication;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public class RouteManagerViewModel : ViewModelBase
    {
        private readonly RouteService _routeService;
        private readonly StationService _stationService;

        private RouteViewModel _selectedRoute;
        private Station _selectedStation;
        private State _currentState;

        public ObservableCollection<RouteViewModel> Routes { get; set; }
        public RouteViewModel SelectedRoute
        {
            get => _selectedRoute;
            set { _selectedRoute = value; NotifyPropertyChanged(nameof(SelectedRoute)); }
        }
        public Station SelectedStation
        {
            get => _selectedStation;
            set { _selectedStation = value; NotifyPropertyChanged(nameof(SelectedStation)); }
        }
        public State CurrentState
        {
            get => _currentState;
            set { _currentState = value; NotifyPropertyChanged(nameof(CurrentState)); }
        }

        public ObservableCollection<Station> Stations { get; set; }

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
            => new RelayCommand(MoveToRoute);
        public ICommand RemoveFromRouteCommand
            => new RelayCommand(RemoveFromRoute);
        public ICommand MoveUpCommand
            => new RelayCommand(MoveUp);
        public ICommand MoveDownCommand
            => new RelayCommand(MoveDown);

        public RouteManagerViewModel(RouteService routeService, StationService stationService)
        {
            _routeService = routeService;
            _stationService = stationService;

            Routes = new ObservableCollection<RouteViewModel>();
            foreach (Route route in _routeService.GetAll())
                Routes.Add(new RouteViewModel(route));

            Stations = new ObservableCollection<Station>(_stationService.GetAll());
        }

        private void Add()
        {
            SelectedRoute = new RouteViewModel(new Route());
            CurrentState = State.Add;
        }

        private void Delete()
        {
            if(SelectedRoute == null) return;
            _routeService.Delete(SelectedRoute.Route);
            Routes.Remove(SelectedRoute);
        }

        private void Edit()
        {
            CurrentState = State.Edit;
        }

        private void Save()
        {
            SelectedRoute.Route.Stations = Stations;
            if (CurrentState == State.Add)
            {
                _routeService.Add(SelectedRoute.Route);
                Routes.Add(SelectedRoute);
            }
            else if (CurrentState == State.Edit)
            {
                _routeService.Update(SelectedRoute.Route);
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
            if (SelectedRoute == null) return;
            if (SelectedStation == null) return;
            SelectedRoute.Stations.Add(SelectedStation);
            Stations.Remove(SelectedStation);
        }
        private void RemoveFromRoute()
        {
            if (SelectedRoute == null) return;
            if (SelectedRoute.SelectedStation == null) return;
            Stations.Add(SelectedRoute.SelectedStation);
            SelectedRoute.Stations.Remove(SelectedRoute.SelectedStation);
        }
        private void MoveUp()
        {
            if (SelectedRoute == null) return;
            if (SelectedRoute.SelectedStation == null) return;
            int index = SelectedRoute.Stations.IndexOf(SelectedRoute.SelectedStation);
            if (index == 0) return;
            SelectedRoute.Stations.Move(index, index - 1);
        }
        private void MoveDown()
        {
            if (SelectedRoute == null) return;
            if (SelectedRoute.SelectedStation == null) return;
            int index = SelectedRoute.Stations.IndexOf(SelectedRoute.SelectedStation);
            if (index == SelectedRoute.Stations.Count - 1) return;
            SelectedRoute.Stations.Move(index, index + 1);
        }
    }
}
