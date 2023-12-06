using Domain.Models;
using Domain.Services;
using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    public class RouteViewModel : ViewModelBase
    {
        private Route _route;
        private RouteService _routeService;

        public int Id
        {
            get => _route.Id;
            set { _route.Id = value; NotifyPropertyChanged(nameof(Id)); }
        }
        public string Name
        {
            get => _route.Name;
            set { _route.Name = value; NotifyPropertyChanged(nameof(Name)); }
        }

        public ObservableCollection<Station> Stations
        {
            get => new ObservableCollection<Station>(_route.Stations);
            set { _route.Stations = value; NotifyPropertyChanged(nameof(Stations)); }
        }
    }
}
