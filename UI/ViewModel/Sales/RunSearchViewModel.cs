using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.Stores;
using UI.ViewModel.Sales;

namespace UI.ViewModel
{
    internal class RunSearchViewModel : ViewModelBase
    {
        private readonly IStationRepository _stationRepository;
        private readonly IRunRepository _runRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly IMessageBoxService _messageBoxService;
        private readonly OrderStore _orderStore;
        private readonly NavigationService _navigationService;

        private ObservableCollection<RunViewModel> _runs;
        private DateTime _departureDateTimeMinimum;
        private DateTime _departureDateTimeMaximum;
        private Station _departureStation;
        private Station _arrivalStation;
        private RunViewModel _selectedRun;

        public ObservableCollection<Station> StationItems { get; set; }
        public ObservableCollection<RunViewModel> Runs
        {
            get => _runs;
            set { _runs = value; OnPropertyChanged(); }
        }
        public DateTime DepartureDateTimeMinimum
        {
            get => _departureDateTimeMinimum;
            set { _departureDateTimeMinimum = value; OnPropertyChanged(); }
        }

        public DateTime DepartureDateTimeMaximum
        {
            get { return _departureDateTimeMaximum; }
            set { _departureDateTimeMaximum = value; OnPropertyChanged(); }
        }
        public Station DepartureStation
        {
            get => _departureStation;
            set { _departureStation = value; OnPropertyChanged(); }
        }
        public Station ArrivalStation
        {
            get => _arrivalStation;
            set { _arrivalStation = value; OnPropertyChanged(); }
        }
        public RunViewModel SelectedRun
        {
            get => _selectedRun;
            set { _selectedRun = value; OnPropertyChanged(); }
        }
        
        public ICommand SellTicketCommand { get; private set; }
        public ICommand FindRunsCommand {  get; private set; }

        public RunSearchViewModel(IStationRepository stationRepository, IRunRepository runRepository,
            NavigationService navigationService, OrderStore orderStore, IRouteRepository routeRepository, 
            IMessageBoxService messageBoxService)
        {
            ArgumentNullException.ThrowIfNull(stationRepository);
            ArgumentNullException.ThrowIfNull(runRepository);
            ArgumentNullException.ThrowIfNull(navigationService);

            _orderStore = orderStore;
            _stationRepository = stationRepository;
            _runRepository = runRepository;
            _routeRepository = routeRepository;
            _navigationService = navigationService;
            _messageBoxService = messageBoxService;

            try
            {
                StationItems = new ObservableCollection<Station>(_stationRepository.GetAll());
            }
            catch (DbUpdateException e)
            {
                StationItems = new ObservableCollection<Station>();
                _messageBoxService.ShowMessage(e.Message);
            }

            Runs = new ObservableCollection<RunViewModel>();

            FindRunsCommand = new RelayCommand(FindRunsMethod);
            SellTicketCommand = new RelayCommand(SellTicket, () => SelectedRun != null && DepartureStation != null && ArrivalStation != null);
        }

        private void FindRunsMethod()
        {
            if (DepartureStation == null)
            {
                _messageBoxService.ShowMessage("Не выбрана станция отправки");
                return;
            }
            if (ArrivalStation == null)
            {
                _messageBoxService.ShowMessage("Не выбрана станция прибытия");
                return;
            }

            List<Run> run = new List<Run>();
            IEnumerable<Route> routes;
            try
            {
                routes = _routeRepository.GetByStations(DepartureStation, ArrivalStation);
                foreach (Route route in routes)
                {
                    run.AddRange(_runRepository.GetByRoute(route));
                }
            }
            catch(DbUpdateException e)
            {
                _messageBoxService.ShowMessage(e.Message);
                return;
            }

            run = run.Where(o => o.DepartureDateTime > DepartureDateTimeMinimum 
                && o.DepartureDateTime < DepartureDateTimeMaximum).ToList();
            
            Runs.Clear(); 
            foreach (var item in run)
            {
                RunViewModel vm = new RunViewModel(item, _runRepository);
                Runs.Add(vm);
            }
        }

        private void SellTicket()
        {
            OrderViewModel order = new OrderViewModel(DepartureStation, ArrivalStation, SelectedRun.GetRun());
            _navigationService.Navigate<PassengerRegistrationViewModel>();
            _orderStore.CreateOrder(order);
        }
    }
}
