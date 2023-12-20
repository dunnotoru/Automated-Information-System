using Domain.Models;
using Domain.RepositoryInterfaces;
using System;

namespace UI.ViewModel.Sales
{
    public class RunViewModel : ViewModelBase
    {
        private readonly IRunRepository _runRepository;

        private int _id;
        private string _number;
        private string _routeName;
        private DateTime _departureDateTime;
        private DateTime _estimatedArrivalDateTime;

        public RunViewModel(Run run, IRunRepository runRepository)
        {
            Id = run.Id;
            Number = run.Number;
            RouteName = run.Route.Name;
            DepartureDateTime = run.DepartureDateTime;
            EstimatedArrivalDateTime = run.EstimatedArrivalDateTime;

            _runRepository = runRepository;
        }

        public Run GetRun()
        {
            return _runRepository.GetById(Id);
        }

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public string Number
        {
            get { return _number; }
            set { _number = value; OnPropertyChanged(); }
        }

        public string RouteName
        {
            get { return _routeName; }
            set { _routeName = value; OnPropertyChanged(); }
        }

        public DateTime DepartureDateTime
        {
            get { return _departureDateTime; }
            set { _departureDateTime = value; OnPropertyChanged(); }
        }


        public DateTime EstimatedArrivalDateTime
        {
            get { return _estimatedArrivalDateTime; }
            set { _estimatedArrivalDateTime = value; OnPropertyChanged(); }
        }

    }
}
