using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.Stores;

namespace UI.ViewModel
{
    internal class RunSearchViewModel : ViewModelBase
    {
        public ObservableCollection<Station> StationItems { get; set; }
        public ObservableCollection<Run> RunItems { get; set; }

        private DateTime _departureDateTime;
        private DateTime _arrivalDateTime;
        private Station _departureStation;
        private Station _arrivalStation;
        private Run _selectedRun;

        public DateTime DepartureDateTime
        {
            get => _departureDateTime;
            set
            {
                _departureDateTime = value;
                NotifyPropertyChanged(nameof(DepartureDateTime));
            }
        }
        public DateTime ArrivalDateTime
        {
            get => _arrivalDateTime;
            set
            {
                _arrivalDateTime = value;
                NotifyPropertyChanged(nameof(ArrivalDateTime));
            }
        }
        public Station DepartureStation
        {
            get => _departureStation;
            set
            {
                _departureStation = value;
                NotifyPropertyChanged(nameof(DepartureStation));
            }
        }
        public Station ArrivalStation
        {
            get => _arrivalStation;
            set
            {
                _arrivalStation = value;
                NotifyPropertyChanged(nameof(ArrivalStation));
            }
        }
        public Run SelectedRun
        {
            get => _selectedRun;
            set
            {
                _selectedRun = value;
                NotifyPropertyChanged(nameof(SelectedRun));
            }
        }

        public RelayCommand FindRunsCommand
        {
            get => new RelayCommand(FindRunsMethod);
        }

        private void FindRunsMethod()
        {
            
        }

        private readonly IStationRepository _stationRepository;
        private readonly IRunRepository _runRepository;
        private readonly OrderStore _orderStore;
        private readonly NavigationService _navigationService;
        public ICommand SellTicketCommand
        {
            get => new RelayCommand(SellTicket);
        }

        public RunSearchViewModel(IStationRepository stationRepository, IRunRepository runRepository,
            NavigationService navigationService, OrderStore orderStore)
        {
            ArgumentNullException.ThrowIfNull(stationRepository);
            ArgumentNullException.ThrowIfNull(runRepository);
            ArgumentNullException.ThrowIfNull(navigationService);

            _orderStore = orderStore;
            _stationRepository = stationRepository;
            _runRepository = runRepository;
            _navigationService = navigationService;

            StationItems = new ObservableCollection<Station>(_stationRepository.GetAll());
            RunItems = new ObservableCollection<Run>();
        }

        private void SellTicket()
        {
            OrderViewModel order = new OrderViewModel(DepartureStation, ArrivalStation, SelectedRun);
            _navigationService.Navigate<PassengerRegistrationViewModel>();
            _orderStore.CreateOrder(order);
        }
    }
}
