using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class StationEditViewModel : EditViewModel
{
    private string _name = string.Empty;
    private string _address = string.Empty;
    
    public override ICommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Station
        {
            Id = this.Id,
            Name = _name,
            Address = _address
        }), CanSave);
    
    public override ICommand RemoveCommand => new RelayCommand(ExecuteRemove<Station>);
    
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
        set { _name = value; NotifyPropertyChanged(); }
    }
    
    public string Address
    {
        get => _address;
        set { _address = value; NotifyPropertyChanged(); }
    }
}