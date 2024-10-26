using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class RunFormViewModel : EditViewModel
{
    private Run _run;
    
    private ObservableCollection<RouteViewModel> _routeItems;
    private ObservableCollection<VehicleViewModel> _vehicleItems;
    private ObservableCollection<DriverViewModel> _driverItems;
    
    private RouteViewModel _selectedRoute;
    private VehicleViewModel _selectedVehicle;
    private DriverViewModel _selectedDriver;
    
    protected override int? Save(DomainContext context)
    {
        _run.RouteId = _run.RouteId;
        _run.VehicleId = _run.VehicleId;
        _run.DriverId = _run.DriverId;
        context.Runs.Update(_run);
        context.SaveChanges();
        return _run.RouteId;
    }

    protected override void Remove(DomainContext context)
    {
        throw new NotImplementedException();
    }

    protected override bool CanSave()
    {
        throw new NotImplementedException();
    }
    
    public RunFormViewModel(Run run, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _run = run;
        Id = _run.Id;
    }
    
    public RunFormViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _run = new Run();
    }

    public ObservableCollection<RouteViewModel> RouteItems
    {
        get => _routeItems;
        set { _routeItems = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<VehicleViewModel> VehicleItems
    {
        get => _vehicleItems;
        set { _vehicleItems = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<DriverViewModel> DriverItems
    {
        get => _driverItems;
        set { _driverItems = value; RaisePropertyChanged(); }
    }

    public RouteViewModel SelectedRoute
    {
        get => _selectedRoute;
        set { _selectedRoute = value; RaisePropertyChanged(); }
    }

    public VehicleViewModel SelectedVehicle
    {
        get => _selectedVehicle;
        set { _selectedVehicle = value; RaisePropertyChanged(); }
    }

    public DriverViewModel SelectedDriver
    {
        get => _selectedDriver;
        set { _selectedDriver = value; RaisePropertyChanged(); }
    }

    public string Number
    {
        get => _run.Number;
        set { _run.Number = value; RaisePropertyChanged(); }
    }

    public DateTime DepartureDate
    {
        get => _run.DepartureDateTime;
        set { _run.DepartureDateTime = value; RaisePropertyChanged(); }
    }
    
    public DateTime ArrivalDate
    {
        get => _run.EstimatedArrivalDateTime;
        set { _run.EstimatedArrivalDateTime = value; RaisePropertyChanged(); }
    }
}