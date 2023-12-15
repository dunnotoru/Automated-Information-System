using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.Stores;

namespace UI.ViewModel
{
    internal class RunSearchViewModel : ViewModelBase
    { 
        public ObservableCollection<Station> StationItems { get; set; }
        
        private ObservableCollection<Run> _runItems;
        public ObservableCollection<Run> RunItems
        {
            get => _runItems;
            set
            {
                _runItems = value; OnPropertyChanged();
            }
        }

        private DateTime _departureDateTime;
        private Station _departureStation;
        private Station _arrivalStation;
        private Run _selectedRun;

        public DateTime DepartureDateTime
        {
            get => _departureDateTime;
            set
            {
                _departureDateTime = value;
                OnPropertyChangedByName(nameof(DepartureDateTime));
            }
        }
        public Station DepartureStation
        {
            get => _departureStation;
            set
            {
                _departureStation = value;
                OnPropertyChangedByName(nameof(DepartureStation));
            }
        }
        public Station ArrivalStation
        {
            get => _arrivalStation;
            set
            {
                _arrivalStation = value;
                OnPropertyChangedByName(nameof(ArrivalStation));
            }
        }
        public Run SelectedRun
        {
            get => _selectedRun;
            set
            {
                _selectedRun = value;
                OnPropertyChangedByName(nameof(SelectedRun));
            }
        }
        

        private readonly IStationRepository _stationRepository;
        private readonly IRunRepository _runRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly IMessageBoxService _messageBoxService;
        private readonly OrderStore _orderStore;
        private readonly NavigationService _navigationService;
        
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

            RunItems = new ObservableCollection<Run>();

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
                RunItems = new ObservableCollection<Run>();
                return;
            }
            
            RunItems = new ObservableCollection<Run>(run);
        }

        private void SellTicket()
        {
            OrderViewModel order = new OrderViewModel(DepartureStation, ArrivalStation, SelectedRun);
            _navigationService.Navigate<PassengerRegistrationViewModel>();
            _orderStore.CreateOrder(order);
        }
    }
}
