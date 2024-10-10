using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;
using InformationSystem.ViewModel.Sales;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class RouteEditViewModel : EditViewModel
{
    private readonly Route _route;
    private ObservableCollection<StationViewModel> _stationItems;
    
    private StationViewModel _selectedStation;
    
    protected override int? Save(DomainContext context)
    {
        context.Routes.Update(_route);
        context.SaveChanges();
        context.Entry(_route).Reload();
        return _route.Id;
    }

    protected override void Remove(DomainContext context)
    {
        context.Routes.Remove(_route);
        context.SaveChanges();
    }

    protected override bool CanSave()
    {
        return true;
    }

    public RouteEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _route = new Route();
        
        using DomainContext context = contextFactory.CreateDbContext();
        _stationItems = new ObservableCollection<StationViewModel>(
            context.Stations.Select(s => new StationViewModel(s)));
    }
    
    public RouteEditViewModel(Route route, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _route = route;
        Id = _route.Id;
        
        using DomainContext context = contextFactory.CreateDbContext();
        _stationItems = new ObservableCollection<StationViewModel>(
            context.Stations.Select(s => new StationViewModel(s)));
    }

    public string Name
    {
        get => _route.Name;
        set { _route.Name = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<StationViewModel> StationItems
    {
        get => _stationItems;
        set { _stationItems = value; RaisePropertyChanged(); }
    }

    public StationViewModel SelectedStation
    {
        get => _selectedStation;
        set { _selectedStation = value; RaisePropertyChanged(); }
    }
}