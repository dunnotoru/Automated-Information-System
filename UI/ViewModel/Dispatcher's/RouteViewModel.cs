using Domain.Models;
using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    public class RouteViewModel : ViewModelBase
    {
        private Station _selectedStation;
        public Route Route { get; set; }

        public RouteViewModel(Route route)
        {
            Route = route;
            Stations = new ObservableCollection<Station>(Route.Stations);
        }

        public int Id
        {
            get => Route.Id;
            set { Route.Id = value; NotifyPropertyChanged(nameof(Id)); }
        }
        public string Name
        {
            get => Route.Name;
            set { Route.Name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public ObservableCollection<Station> Stations { get; set; }
        public Station SelectedStation
        {
            get => _selectedStation;
            set { _selectedStation=value; NotifyPropertyChanged(nameof(SelectedStation));}
        }

    }
}
