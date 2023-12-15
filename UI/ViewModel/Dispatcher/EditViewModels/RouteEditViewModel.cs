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
		
		private ObservableCollection<StationItemViewModel> _availableStations;
		private ObservableCollection<StationItemViewModel> _stations;
		private StationItemViewModel _selectedAvailableStation;
		private StationItemViewModel _selectedStation;

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
            Name = route.Name ?? "";
            Stations = new ObservableCollection<StationItemViewModel>();
			

            foreach (Station item in route.Stations)
                Stations.Add(new StationItemViewModel(item));

            _routeRepository = routeRepository;
            _stationRepository = stationRepository;

            AvailableStations = new ObservableCollection<StationItemViewModel>();

            IEnumerable<Station> tempStations = _stationRepository.GetAll();

            tempStations = tempStations.Where(o => !Stations.Any(x => o.Id == x.Id)).ToList();

            foreach (Station item in tempStations)
                AvailableStations.Add(new StationItemViewModel(item));
        }

        public RouteEditViewModel(IRouteRepository routeRepository, IStationRepository stationRepository) : this(stationRepository)
		{
			ArgumentNullException.ThrowIfNull(routeRepository);

            Id = 0;
            Name = "unknown";
            Stations = new ObservableCollection<StationItemViewModel>();

            _routeRepository = routeRepository;
            _stationRepository = stationRepository;

            AvailableStations = new ObservableCollection<StationItemViewModel>();

            IEnumerable<Station> tempStations = _stationRepository.GetAll();

            foreach (Station item in tempStations)
                AvailableStations.Add(new StationItemViewModel(item));
        }

        private RouteEditViewModel(IStationRepository stationRepository)
        {
            SaveCommand = new RelayCommand(Save);
            RemoveCommand = new RelayCommand(Remove);
            AddStationCommand = new RelayCommand(AddStation);
            RemoveStationCommand = new RelayCommand(RemoveStation);
            MoveUpCommand = new RelayCommand(MoveUp);
            MoveDownCommand = new RelayCommand(MoveDown);
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

		public ObservableCollection<StationItemViewModel> Stations
		{
			get { return _stations; }
			set { _stations = value; OnPropertyChanged(); }
		}

		public ObservableCollection<StationItemViewModel> AvailableStations
		{
			get { return _availableStations; }
			set { _availableStations = value; OnPropertyChanged(); }
		}

		public StationItemViewModel SelectedAvailableStation
		{
			get => _selectedAvailableStation;
			set { _selectedAvailableStation = value; OnPropertyChanged(); }
		}

		public StationItemViewModel SelectedStation
		{
			get => _selectedStation;
			set { _selectedStation = value; OnPropertyChanged(); }
		}

		private void Save()
		{
			Collection<Station> stations = new Collection<Station>();
			
			foreach (StationItemViewModel item in Stations) 
				stations.Add(item.Station);

			Route route = new Route()
			{
				Id = Id,
				Name = Name,
				Stations = stations
			};

			try
			{
				if(Id == 0)
				{
					_routeRepository.Add(route);
				}
				else
				{
					_routeRepository.Update(Id, route);
				}
			}
			catch(Exception e)
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
			if(SelectedAvailableStation == null) return;

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
