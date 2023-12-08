using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
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

        private readonly IStationRepository _stationRepository;
        private readonly IRunRepository _runRepository;
        public NavigateCommand SellTicketCommand { get; }

        public RunSearchViewModel(IStationRepository stationRepository, IRunRepository runRepository,
            NavigationService toPassengerRegistration)
        {
            ArgumentNullException.ThrowIfNull(stationRepository);
            ArgumentNullException.ThrowIfNull(runRepository);
            ArgumentNullException.ThrowIfNull(toPassengerRegistration);

            _stationRepository = stationRepository;
            _runRepository = runRepository;
            SellTicketCommand = new NavigateCommand(toPassengerRegistration);

            StationItems = new ObservableCollection<Station>(_stationRepository.GetAll());
            RunItems = new ObservableCollection<Run>();
        }
    }
}
