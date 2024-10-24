using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Transactions;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class RouteEditViewModel : EditViewModel
{
    private readonly Route _route;

    public StationForRouteSelectionViewModel StationSelectionViewModel { get; }

    protected override int? Save(DomainContext context)
    {
        context.Routes.Update(_route);
        context.SaveChanges();
        
        ObservableCollection<StationViewModel> stationViewModels = StationSelectionViewModel.UsedStations;
        List<StationRoute> storedStationRoutes = context.StationRoute
            .Where(sr => sr.RouteId == _route.Id).ToList();

        for (int index = 0; index < stationViewModels.Count; index++)
        {
            StationViewModel stationVm = stationViewModels[index];
            StationRoute? sr = storedStationRoutes.SingleOrDefault(sr => sr.StationId == stationVm.Id);
            if (sr is null)
            {
                sr = new StationRoute
                {
                    RouteId = _route.Id,
                    StationId = stationVm.Id,
                    Order = index 
                };
                context.Add(sr);
            }
            else if (sr.Order != index)
            {
                sr.Order = index;
                context.Update(sr);
            }
            storedStationRoutes.Remove(sr);
        }

        context.StationRoute.RemoveRange(storedStationRoutes);

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
        return string.IsNullOrWhiteSpace(Name) && StationSelectionViewModel.UsedStations.Count > 1;
    }

    public RouteEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _route = new Route();
        using DomainContext context = contextFactory.CreateDbContext();
        
        StationSelectionViewModel = new StationForRouteSelectionViewModel(
            new ObservableCollection<StationViewModel>(context.Stations.Select(s => new StationViewModel(s))),
            new ObservableCollection<StationViewModel>()
        );
    }
    
    public RouteEditViewModel(Route route, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _route = route;
        Id = _route.Id;
        using DomainContext context = contextFactory.CreateDbContext();
        context.Entry(_route).Reload();
        context.Entry(_route).Collection(r=>r.Stations).Load();
        List<Station> stations = context.Stations.Include(s => s.Routes).ToList();
        
        StationSelectionViewModel = new StationForRouteSelectionViewModel(
            new ObservableCollection<StationViewModel>(stations
                .Except(_route.Stations)
                .Select(s => new StationViewModel(s))),
            new ObservableCollection<StationViewModel>(_route
                .Stations
                .Select(s => new StationViewModel(s)))
        );
    }

    public string Name
    {
        get => _route.Name;
        set { _route.Name = value; RaisePropertyChanged(); }
    }
}