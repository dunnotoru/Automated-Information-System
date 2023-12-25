using Domain.Models;
using Domain.RepositoryInterfaces;
using System;

namespace UI.ViewModel.Sales
{
    public class RunViewModel : ViewModelBase
    {
        private int _id;
        private string _number;
        private string _routeName;
        private DateTime _departureDateTime;
        private DateTime _estimatedArrivalDateTime;

        public RunViewModel(Run run)
        {
            ArgumentNullException.ThrowIfNull(run);

            Id = run.Id;
            Number = run.Number;
            RouteName = run.Route.Name;
            DepartureDateTime = run.DepartureDateTime;
            EstimatedArrivalDateTime = run.EstimatedArrivalDateTime;

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
