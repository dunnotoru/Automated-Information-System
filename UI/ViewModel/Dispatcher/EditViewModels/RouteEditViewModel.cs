using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    class RouteEditViewModel : ViewModelBase
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IStationRepository _stationRepository;

        private int _id;
        private string _name;

        private ObservableCollection<StationViewModel> _availableStations;
        private ObservableCollection<StationViewModel> _stations;
        private StationViewModel _selectedAvailableStation;
        private StationViewModel _selectedStation;

        public Action<RouteEditViewModel> RemoveEvent;
        public Action<string> ErrorEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }
        public ICommand AddStationCommand { get; }
        public ICommand RemoveStationCommand { get; }
        public ICommand MoveUpCommand { get; }
        public ICommand MoveDownCommand { get; }

        public RouteEditViewModel(Route route, IRouteRepository routeRepository, IStationRepository stationRepository) : this(stationRepository)
        {
            ArgumentNullException.ThrowIfNull(route);
            ArgumentNullException.ThrowIfNull(routeRepository);

            Id = route.Id;
            Name = route.Name;
            Stations = new ObservableCollection<StationViewModel>();

            _routeRepository = routeRepository;
            _stationRepository = stationRepository;

            foreach (Station item in route.Stations)
                Stations.Add(new StationViewModel(item, _stationRepository));


            AvailableStations = new ObservableCollection<StationViewModel>();

            IEnumerable<Station> tempStations = _stationRepository.GetAll();

            tempStations = tempStations.Where(o => !Stations.Any(x => o.Id == x.Id)).ToList();

            foreach (Station item in tempStations)
                AvailableStations.Add(new StationViewModel(item, _stationRepository));
        }

        public RouteEditViewModel(IRouteRepository routeRepository, IStationRepository stationRepository) : this(stationRepository)
        {
            ArgumentNullException.ThrowIfNull(routeRepository);

            Id = 0;
            Name = "";
            Stations = new ObservableCollection<StationViewModel>();

            _routeRepository = routeRepository;
            _stationRepository = stationRepository;

            AvailableStations = new ObservableCollection<StationViewModel>();

            IEnumerable<Station> tempStations = _stationRepository.GetAll();

            foreach (Station item in tempStations)
                AvailableStations.Add(new StationViewModel(item, _stationRepository));
        }

        private RouteEditViewModel(IStationRepository stationRepository)
        {
            SaveCommand = new RelayCommand(Save, () => CanSave());
            RemoveCommand = new RelayCommand(Remove);
            AddStationCommand = new RelayCommand(AddStation);
            RemoveStationCommand = new RelayCommand(RemoveStation);
            MoveUpCommand = new RelayCommand(MoveUp);
            MoveDownCommand = new RelayCommand(MoveDown);
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                Stations.Count >= 2;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public ObservableCollection<StationViewModel> Stations
        {
            get { return _stations; }
            set { _stations = value; OnPropertyChanged(); }
        }

        public ObservableCollection<StationViewModel> AvailableStations
        {
            get { return _availableStations; }
            set { _availableStations = value; OnPropertyChanged(); }
        }

        public StationViewModel SelectedAvailableStation
        {
            get => _selectedAvailableStation;
            set { _selectedAvailableStation = value; OnPropertyChanged(); }
        }

        public StationViewModel SelectedStation
        {
            get => _selectedStation;
            set { _selectedStation = value; OnPropertyChanged(); }
        }

        private void Save()
        {
            Collection<Station> stations = new Collection<Station>();

            foreach (StationViewModel item in Stations)
                stations.Add(item.GetStation());

            Route route = new Route()
            {
                Id = Id,
                Name = Name,
                Stations = stations
            };

            try
            {
                if (Id == 0)
                {
                    _routeRepository.Create(route);
                }
                else
                {
                    _routeRepository.Update(Id, route);
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
                _routeRepository.Remove(Id);
                RemoveEvent?.Invoke(this);
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }

        private void AddStation()
        {
            if (SelectedAvailableStation == null) return;

            Stations.Add(SelectedAvailableStation);
            AvailableStations.Remove(SelectedAvailableStation);
        }

        private void RemoveStation()
        {
            if (SelectedStation == null) return;

            AvailableStations.Add(SelectedStation);
            Stations.Remove(SelectedStation);
        }

        private void MoveUp()
        {
            if (SelectedStation == null) return;
            int index = Stations.IndexOf(SelectedStation);
            if (index == 0) return;
            Stations.Move(index, index - 1);
        }

        private void MoveDown()
        {
            if (SelectedStation == null) return;
            int index = Stations.IndexOf(SelectedStation);
            if (index == Stations.Count - 1) return;
            Stations.Move(index, index + 1);
        }
    }
}
