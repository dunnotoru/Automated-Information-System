using Domain.Models;
using Domain.RepositoryInterfaces;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    internal class RouteViewModel : ViewModelBase
    {
        private readonly IRouteRepository _routeRepository;
        private int _id;
        private string _name;

        public RouteViewModel(Route route, IRouteRepository routeRepository)
        {
            Id = route.Id;
            Name = route.Name;
            _routeRepository = routeRepository;
        }

        public RouteViewModel(IRouteRepository routeRepository)
        {
            Id = 0;
            Name = "";
            _routeRepository = routeRepository;
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public Route GetRoute()
        {
            return _routeRepository.GetById(Id);
        }
    }
}