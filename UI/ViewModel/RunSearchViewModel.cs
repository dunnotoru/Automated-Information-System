using Domain.Models;
using Domain.UseCases.CashierUseCases;
using Domain.UseCases.CasshierUseCases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel
{
    public class RunSearchViewModel : ViewModelBase
    {
        public ObservableCollection<Station> StationItems {  get; set; }
        public ObservableCollection<Run> RunItems { get; set; }

        private string _stationSearch;
        public string StationSearch
        {
            get { return _stationSearch; }
            set
            {
                _stationSearch = value;
                NotifyPropertyChanged(nameof(StationSearch));
            }
        }

        private Station _departureStation;
        public Station DepartureStation
        {
            get => _departureStation;
            set
            {
                _departureStation = value;
                NotifyPropertyChanged(nameof(DepartureStation));
            }
        }
        
        private Station _arrivalStation;
        public Station ArrivalStation
        {
            get => _arrivalStation;
            set
            {
                _arrivalStation = value;
                NotifyPropertyChanged(nameof(ArrivalStation));
            }
        }

        public RelayCommand FindRunsCommand
        {
            get => new RelayCommand(FindRunsMethod);
        }

        private void FindRunsMethod()
        {
            //List<Station> stations = _getStations.GetStations().ToList();
            
            //Station departure = stations.First(x => x == DepartureStation);
            //Station arrival = stations.First(x => x == DepartureStation);

            List<Run> s = _findRuns.FindRuns(null, null, DateTime.MinValue).ToList();
            RunItems = new ObservableCollection<Run>(s);
            NotifyPropertyChanged(nameof(RunItems));
        }

        private readonly GetStationsUseCase _getStations;
        private readonly FindRunsUseCase _findRuns;
        public NavigateCommand SellTicketCommand { get; }

        public RunSearchViewModel(GetStationsUseCase getStations, FindRunsUseCase findRuns,
            NavigationService ticketSaleNavigationService)
        {
            _getStations = getStations;
            _findRuns = findRuns;

            StationItems = new ObservableCollection<Station>(_getStations.GetStations());
            RunItems = new ObservableCollection<Run>();
            SellTicketCommand = new NavigateCommand(ticketSaleNavigationService);
        }
    }
}
