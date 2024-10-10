using System;
using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class StationViewModel : ViewModelBase
{
    public int Id { get; }
    private string _name;
    private string _address;

    public StationViewModel(Station station)
    {
        Id = station.Id;
        _name = station.Name;
        _address = station.Address;
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