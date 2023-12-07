using Domain.Models;
using Domain.Services;
using System.Collections.ObjectModel;

namespace UI.ViewModel
{
    public class RunManagerViewModel : ViewModelBase
    {
        private readonly RunService _runService;
        private readonly RouteService _routeService;

        public ObservableCollection<Run> Runs { get; set; }
        public ObservableCollection<Route> Routes { get; set; }

        public RunManagerViewModel(RunService runService, RouteService routeService)
        {
            _runService = runService;
            _routeService = routeService;

            Runs = new ObservableCollection<Run>(_runService.GetAll());
            Routes = new ObservableCollection<Route>(_routeService.GetAll());
        }
    }
}
