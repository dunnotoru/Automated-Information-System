using Domain.Services;

namespace UI.ViewModel.Dispatcher_s
{
    public class RunManagerViewModel : ViewModelBase
    {
        private readonly RunService _runService;
        private readonly RouteService _routeService;





        public RunManagerViewModel(RunService runService, RouteService routeService)
        {
            _runService = runService;
            _routeService = routeService;
        }
    }
}
