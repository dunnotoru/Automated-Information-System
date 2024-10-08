using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Services;
using InformationSystem.Services.Abstractions;
using InformationSystem.Stores;
using InformationSystem.ViewModel.HelperViewModels;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Sales;

internal class RunSearchViewModel : ObservableObject
{
    private readonly IMessageBoxService _messageBoxService;
    private readonly RunSearchService _runSearchService;
    private readonly OrderStore _orderStore;
    private readonly NavigationService _navigationService;
    
    private readonly IDbContextFactory<DomainContext> _contextFactory;

    private ObservableCollection<StationViewModel> _stations;
    private ObservableCollection<StationViewModel> _departureStations;
    private ObservableCollection<StationViewModel> _arrivalStations;
    private ObservableCollection<RunViewModel> _runs;
    private DateTime _departureDateTimeMinimum;
    private DateTime _departureDateTimeMaximum;
    private StationViewModel _departureStation;
    private StationViewModel _arrivalStation;
    private RunViewModel _selectedRun;
    private int _freePlaces;

    private string _departureStationSearch;
    private string _arrivalStationSearch;

    public ICommand SellTicketCommand { get; private set; }
    public ICommand FindRunsCommand { get; private set; }

    public RunSearchViewModel(NavigationService navigationService, OrderStore orderStore, 
        IMessageBoxService messageBoxService, RunSearchService runSearchService, IDbContextFactory<DomainContext> contextFactory)
    {
        _orderStore = orderStore;
        _navigationService = navigationService;
        _messageBoxService = messageBoxService;
        _runSearchService = runSearchService;
        _contextFactory = contextFactory;

        _stations = new ObservableCollection<StationViewModel>();

        DepartureStations = new ObservableCollection<StationViewModel>(_stations);
        ArrivalStations = new ObservableCollection<StationViewModel>(_stations);

        Runs = new ObservableCollection<RunViewModel>();

        DepartureDateTimeMinimum = DateTime.Now;
        DepartureDateTimeMaximum = DateTime.MaxValue;

        FindRunsCommand = new RelayCommand(FindRunsMethod, CanFindRuns);
        SellTicketCommand = new RelayCommand(SellTicket, () => SelectedRun != null && DepartureStation != null && ArrivalStation != null);
    }

    private bool CanFindRuns() => DepartureStation != null && ArrivalStation != null;

    private void FindRunsMethod()
    {
        throw new NotImplementedException();
    }

    private void SellTicket()
    {
        OrderViewModel? order = null;
        using (var context = _contextFactory.CreateDbContext())
        {
            var departure = context.Stations.Find(DepartureStation.Id)!;
            var arrival = context.Stations.Find(ArrivalStation.Id)!;
            order = new OrderViewModel(departure, arrival, context.Runs.Find(SelectedRun.Id)!);
        }
        _navigationService.Navigate<PassengerRegistrationViewModel>();
        _orderStore.CreateOrder(order);
    }
    private void FilterStations(ObservableCollection<StationViewModel> stations, string substring)
    {
        stations.Clear();
        foreach (var item in _stations.Where(o => o.Name.ToLower().Contains(substring.ToLower())
                                                  || o.Address.ToLower().Contains(substring.ToLower())))
        {
            stations.Add(item);
        }
    }
        
    public int FreePlaces
    {
        get { return _freePlaces; }
        set { _freePlaces = value; OnPropertyChanged(); }
    }

    public ObservableCollection<StationViewModel> DepartureStations
    {
        get { return _departureStations; }
        set { _departureStations = value; OnPropertyChanged(); }
    }
    public ObservableCollection<StationViewModel> ArrivalStations
    {
        get { return _arrivalStations; }
        set { _arrivalStations = value; }
    }

    public ObservableCollection<RunViewModel> Runs
    {
        get => _runs;
        set { _runs = value; OnPropertyChanged(); }
    }
    public DateTime DepartureDateTimeMinimum
    {
        get => _departureDateTimeMinimum;
        set { _departureDateTimeMinimum = value; OnPropertyChanged(); }
    }
    public DateTime DepartureDateTimeMaximum
    {
        get { return _departureDateTimeMaximum; }
        set { _departureDateTimeMaximum = value; OnPropertyChanged(); }
    }
    public StationViewModel DepartureStation
    {
        get => _departureStation;
        set { _departureStation = value; OnPropertyChanged();  }
    }
    public StationViewModel ArrivalStation
    {
        get => _arrivalStation;
        set { _arrivalStation = value; OnPropertyChanged();  }
    }
    public RunViewModel SelectedRun
    {
        get => _selectedRun;
        set { _selectedRun = value; OnPropertyChanged(); }
    }

    public string DepartureStationSearch
    {
        get { return _departureStationSearch; }
        set { _departureStationSearch = value; OnPropertyChanged(); FilterStations(DepartureStations, _departureStationSearch); }
    }
    public string ArrivalStationSearch
    {
        get { return _arrivalStationSearch; }
        set { _arrivalStationSearch = value; OnPropertyChanged(); FilterStations(ArrivalStations, _arrivalStationSearch); }
    }
}