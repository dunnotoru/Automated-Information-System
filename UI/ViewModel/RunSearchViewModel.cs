using Domain.Models;
using Domain.UseCases.CasshierUseCases;
using System.Collections.ObjectModel;
using UI.Command;

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

        private string _departureStation;
        public string DepartureStation
        {
            get => _departureStation;
            set
            {
                _departureStation = value;
                NotifyPropertyChanged(nameof(DepartureStation));
            }
        }
        
        private string _arrivalStation;
        public string ArrivalStation
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

        }

        public RelayCommand SellTicket
        {
            get => new RelayCommand(SellTicketMethod);
        }

        private void SellTicketMethod()
        {

        }

        private readonly GetStationsUseCase _getStations;
        private readonly FindRunsUseCase _findRuns;

        public RunSearchViewModel(GetStationsUseCase getStations, FindRunsUseCase findRuns)
        {
            _getStations = getStations;
            _findRuns = findRuns;

            StationItems = new ObservableCollection<Station>(_getStations.GetStations());
            RunItems = new ObservableCollection<Run>();
        }
    }
}
