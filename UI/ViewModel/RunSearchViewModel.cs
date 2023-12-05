using Domain.Models;
using Domain.UseCases.CashierUseCases;
using Domain.UseCases.CasshierUseCases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UI.Command;
using UI.Services;

namespace UI.ViewModel
{
    public class RunSearchViewModel : ViewModelBase
    {
        public ObservableCollection<Station> StationItems { get; set; }
        public ObservableCollection<Run> RunItems { get; set; }

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
