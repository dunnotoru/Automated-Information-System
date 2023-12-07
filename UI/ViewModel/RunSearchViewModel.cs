using Domain.Models;
using Domain.Services;
using Domain.UseCases.CashierUseCases;
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
            
        }

        private readonly StationService _stationService;
        private readonly RunService _runService;
        public NavigateCommand SellTicketCommand { get; }

        public RunSearchViewModel(StationService stationService, RunService runService,
            NavigationService toPassengerRegistration)
        {
            ArgumentNullException.ThrowIfNull(toPassengerRegistration);
            ArgumentNullException.ThrowIfNull(stationService);
            ArgumentNullException.ThrowIfNull(runService);
            _stationService = stationService;
            _runService = runService;
            SellTicketCommand = new NavigateCommand(toPassengerRegistration);

            StationItems = new ObservableCollection<Station>(_stationService.GetAll());
            RunItems = new ObservableCollection<Run>();
        }
    }
}
