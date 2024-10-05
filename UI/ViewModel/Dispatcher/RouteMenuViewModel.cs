using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Domain.Models;
using Domain.RepositoryInterfaces;
using UI.Command;
using UI.Services;
using UI.Services.Abstractions;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel.Dispatcher;

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
        set { _selectedRoute = value; NotifyPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
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
            vm.Remove += OnRemove;
            vm.Error += OnError;
            vm.Save += OnSave;
            Routes.Add(vm);
        }

        AddCommand = new RelayCommand(Add);
    }

    private void Add()
    {
        RouteEditViewModel vm = new RouteEditViewModel(_routeRepository, _stationRepository);
        vm.Remove += OnRemove;
        vm.Error += OnError;
        vm.Save += OnSave;
        Routes.Add(vm);
        SelectedRoute = vm;
    }

    private void OnSave(object? sender, EventArgs e)
    {
        RouteEditViewModel vm = (RouteEditViewModel)sender;

        vm.Remove -= OnRemove;
        vm.Save -= OnSave;
        vm.Error -= OnError;

        Route route = _routeRepository.GetById(vm.Id);
        RouteEditViewModel updatedVm = new RouteEditViewModel(route, _routeRepository, _stationRepository);

        updatedVm.Remove += OnRemove;
        updatedVm.Save += OnSave;
        updatedVm.Error += OnError;

        int index = Routes.IndexOf(vm);
        Routes.Insert(index, updatedVm);
        Routes.Remove(vm);

        _messageBoxService.ShowMessage("Данные успешно сохранены.");
    }

    private void OnRemove(object? sender, EventArgs e)
    {
        RouteEditViewModel vm = (RouteEditViewModel)sender;
        vm.Remove -= OnRemove;
        vm.Error -= OnError;
        vm.Save -= OnSave;
        if (Routes.Remove(vm))
        {
            _messageBoxService.ShowMessage("Данные успешно удалены.");
        }
    }

    private void OnError(object? sender, Exception e)
    {
        _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
    }
}