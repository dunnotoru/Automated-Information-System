using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class StationEditViewModel : EditViewModel
{
    private readonly Station _station;

    protected override int? Save(DomainContext context)
    {
        context.Stations.Update(_station);
        context.SaveChanges();
        return _station.Id;
    }

    protected override void Remove(DomainContext context)
    {
        context.Stations.Remove(_station);
        context.SaveChanges();
    }

    protected override bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(Address) 
               && !string.IsNullOrWhiteSpace(Name); 
    }
    
    public StationEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _station = new Station();
    }
    
    public StationEditViewModel(Station station, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _station = station;
        Id = _station.Id;
    }
    
    public string Name
    {
        get => _station.Name;
        set { _station.Name = value; RaisePropertyChanged(); }
    }
    
    public string Address
    {
        get => _station.Address;
        set { _station.Address = value; RaisePropertyChanged(); }
    }
}