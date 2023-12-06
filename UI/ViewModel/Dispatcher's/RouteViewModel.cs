using Domain.Models;
using Domain.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public class RouteViewModel : ViewModelBase
    {
        private readonly RouteService _routeService;
        private readonly StationService _stationService;
        
        private Route _selectedItem;
        private Station _selectedAllStation;
        private Station _selectedRouteStation;
        private State _currentState;

        public ObservableCollection<Route> Routes { get; set; }
        public ObservableCollection<Station> AllStations { get; set; }
        public ObservableCollection<Station> SelectedRouteStations { get; set; }

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

        public RouteViewModel(RouteService routeService, StationService stationService)
        {
            _routeService = routeService;
            _stationService = stationService;
            CurrentState = State.None;

            Routes = new ObservableCollection<Route>(_routeService.GetAll());
            AllStations = new ObservableCollection<Station>(_stationService.GetAll());
            SelectedRouteStations = new ObservableCollection<Station>();
        }

        private void Add()
        {
            SelectedItem = new Route();
            AllStations = new ObservableCollection<Station>(_stationService.GetAll());
            SelectedRouteStations = new ObservableCollection<Station>();
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
            AllStations = new ObservableCollection<Station>(_stationService.GetAll());
            SelectedRouteStations = new ObservableCollection<Station>(SelectedItem.Stations);
        }

        private void Save()
        {
            SelectedItem.Stations = SelectedRouteStations;
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
    }
}
