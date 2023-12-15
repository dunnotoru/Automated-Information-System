using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel
{
    internal class RouteMenuViewModel : ViewModelBase
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IStationRepository _stationRepository;
        private readonly IMessageBoxService _messageBoxService;

        private RouteEditViewModel _selectedRoute;
        public ObservableCollection<RouteEditViewModel> Routes { get; set; }
        public RouteEditViewModel SelectedRoute
        {
            get => _selectedRoute;
            set { _selectedRoute = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
        }
        
        public bool IsRedactingEnabled => SelectedRoute != null;

        public ICommand AddCommand { get; }

        public RouteMenuViewModel(IRouteRepository routeRepository, IStationRepository stationRepository, IMessageBoxService messageBoxService)
        {
            ArgumentNullException.ThrowIfNull(routeRepository);
            ArgumentNullException.ThrowIfNull(stationRepository);
            _routeRepository = routeRepository;
            _stationRepository = stationRepository;
            _messageBoxService = messageBoxService;

            Routes = new ObservableCollection<RouteEditViewModel>();
            IEnumerable<Route> routes = _routeRepository.GetAll();
            foreach (Route item in routes)
            {
                RouteEditViewModel vm = new RouteEditViewModel(item, _routeRepository, _stationRepository);
                vm.RemoveEvent += OnRemove;
                vm.ErrorEvent += OnError;
                Routes.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void Add()
        {
            RouteEditViewModel vm = new RouteEditViewModel(_routeRepository, _stationRepository);
            vm.RemoveEvent += OnRemove;
            vm.ErrorEvent += OnError;
            Routes.Add(vm);
            SelectedRoute = vm;
        }

        private void OnRemove(RouteEditViewModel vm)
        {
            vm.RemoveEvent -= OnRemove;
            vm.ErrorEvent -= OnError;
            if (Routes.Remove(vm))
            {
                _messageBoxService.ShowMessage("Маршрут удалён");
            }
        }

        private void OnError(string message)
        {
            _messageBoxService.ShowMessage($"Ошибка: {message}");
        }
    }
}
