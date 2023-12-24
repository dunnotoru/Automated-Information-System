using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;
using UI.ViewModel.HelperViewModels;

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

        public event EventHandler Save;
        public event EventHandler Remove;
        public event EventHandler<Exception> Error;

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
                Stations.Add(new StationViewModel(item));


            AvailableStations = new ObservableCollection<StationViewModel>();

            IEnumerable<Station> tempStations = _stationRepository.GetAll();

            tempStations = tempStations.Where(o => !Stations.Any(x => o.Id == x.Id)).ToList();

            foreach (Station item in tempStations)
                AvailableStations.Add(new StationViewModel(item));
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
                AvailableStations.Add(new StationViewModel(item));
        }

        private RouteEditViewModel(IStationRepository stationRepository)
        {
            SaveCommand = new RelayCommand(ExecuteSave, () => CanSave());
            RemoveCommand = new RelayCommand(ExecuteRemove);
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

        private void ExecuteSave()
        {
            Collection<Station> stations = new Collection<Station>();

            foreach (StationViewModel item in Stations)
                stations.Add(_stationRepository.GetById(item.Id));

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
                    Id = _routeRepository.Create(route);
                }
                else
                {
                    _routeRepository.Update(Id, route);
                }
                Save?.Invoke(this, EventArgs.Empty);
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
                _routeRepository.Remove(Id);
                Remove?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Error?.Invoke(this, e);
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
