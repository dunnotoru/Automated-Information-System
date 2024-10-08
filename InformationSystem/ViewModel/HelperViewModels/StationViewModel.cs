using CommunityToolkit.Mvvm.ComponentModel;
using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class StationViewModel : ObservableObject
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
        set { _name = value; OnPropertyChanged(); }
    }

    public string Address
    {
        get => _address;
        set { _address = value; OnPropertyChanged(); }
    }
}