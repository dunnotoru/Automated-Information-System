using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class StationEditViewModel : EditViewModel
{
    private string _name = string.Empty;
    private string _address = string.Empty;
    
    public override IRelayCommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Station
        {
            Id = this.Id,
            Name = _name,
            Address = _address
        }), CanSave);
    
    public override IRelayCommand RemoveCommand => new RelayCommand(ExecuteRemove<Station>);
    
    public StationEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }
    
    public StationEditViewModel(Station station, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = station.Id;
        _name = station.Name;
        _address = station.Address;
    }

    protected override bool CanSave()
    {
        return !string.IsNullOrWhiteSpace(_address) 
               && !string.IsNullOrWhiteSpace(_name); 
    }
    
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
    
    public string Address
    {
        get => _address;
        set => SetProperty(ref _address, value);
    }
}