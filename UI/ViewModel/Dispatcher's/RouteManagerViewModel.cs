using Domain.Models;
using Domain.Services;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Windows.Documents;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public class RouteManagerViewModel : ViewModelBase
    {
        private readonly RouteService _routeService;
        private readonly StationService _stationService;
        
        private Route _selectedItem;
        private Station _selectedAllStation;
        private Station _selectedRouteStation;
        private State _currentState;

        private ObservableCollection<Station> _allStations;
        private ObservableCollection<Station> _routeStations;
        public ObservableCollection<Route> Routes { get; set; }
        public ObservableCollection<Station> AllStations
        {
            get => _allStations;
            set { _allStations = value; NotifyPropertyChanged(nameof(AllStations)); }
        }
        public ObservableCollection<Station> RouteStations
        {
            get => _routeStations;
            set { _routeStations = value; NotifyPropertyChanged(nameof(RouteStations)); }
        }
        public Route SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; NotifyPropertyChanged(nameof(SelectedItem)); }
        }
        public Station SelectedStation
        {
            get => _selectedAllStation;
            set { _selectedAllStation = value; NotifyPropertyChanged(nameof(SelectedStation)); }
        }
        public Station SelectedRouteStation
        {
            get => _selectedRouteStation;
            set { _selectedRouteStation = value; NotifyPropertyChanged(nameof(SelectedRouteStation)); }
        }

        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                NotifyPropertyChanged(nameof(CanChangeSelect));
                NotifyPropertyChanged(nameof(IsReadOnly));
            }
        }

        public bool CanChangeSelect => CurrentState == State.None;
        public bool IsReadOnly => CurrentState == State.None;
        
        public ICommand AddCommand
            => new RelayCommand(Add, () => CurrentState == State.None);
        public ICommand DeleteCommand
            => new RelayCommand(Delete, () => CurrentState == State.None);
        public ICommand EditCommand
            => new RelayCommand(Edit, () => CurrentState == State.None);
        public ICommand SaveCommand
            => new RelayCommand(Save, () => CurrentState == State.Add || CurrentState == State.Edit);

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
            CurrentState = State.None;

            Routes = new ObservableCollection<Route>(_routeService.GetAll());
        }

        private void Add()
        {
            SelectedItem = new Route();
            AllStations = new ObservableCollection<Station>(_stationService.GetAll());  
            RouteStations = new ObservableCollection<Station>();
            CurrentState = State.Add;
        }

        private void Delete()
        {
            _routeService.Delete(SelectedItem);
            if (!Routes.Contains(SelectedItem)) return;
            Routes.Remove(SelectedItem);
        }

        private void Edit()
        {
            CurrentState = State.Edit;

            RouteStations = new ObservableCollection<Station>(SelectedItem.Stations);
            IEnumerable<Station> temp = _stationService.GetAll().Except(SelectedItem.Stations);
            AllStations = new ObservableCollection<Station>(temp);
        }

        private void Save()
        {
            SelectedItem.Stations = RouteStations;
            if (CurrentState == State.Add)
            {
                _routeService.Add(SelectedItem);
                Routes.Add(SelectedItem);
            }
            else if (CurrentState == State.Edit)
            {
                _routeService.Update(SelectedItem);
            }

            CurrentState = State.None;
        }

        private void MoveToRoute()
        {
            if(SelectedStation == null) return;
            RouteStations.Add(SelectedStation);
            AllStations.Remove(SelectedStation);
        }

        private void RemoveFromRoute()
        {
            if (SelectedRouteStation == null) return;
            AllStations.Add(SelectedRouteStation);
            RouteStations.Remove(SelectedRouteStation);
        }

        private void MoveUp()
        {
            if (SelectedRouteStation == null) return;
            int index = RouteStations.IndexOf(SelectedRouteStation);
            if(index == 0) return;

            RouteStations.Move(index, index - 1);
        }

        private void MoveDown()
        {
            if (SelectedRouteStation == null) return;
            int index = RouteStations.IndexOf(SelectedRouteStation);
            if (index == RouteStations.Count - 1) return;
            RouteStations.Move(index, index + 1);
        }
    }
}
