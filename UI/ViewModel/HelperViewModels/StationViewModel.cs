using Domain.Models;
using System;

namespace UI.ViewModel.HelperViewModels;

internal class StationViewModel : ViewModelBase
{
    private int _id;
    private string _name;
    private string _address;

    public int Id
    {
        get { return _id; }
        private set { _id = value; NotifyPropertyChanged(); }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; NotifyPropertyChanged(); }
    }

    public string Address
    {
        get { return _address; }
        set { _address = value; NotifyPropertyChanged(); }
    }

    public StationViewModel(Station station)
    {
        ArgumentNullException.ThrowIfNull(station);

        Id = station.Id;
        Name = station.Name ?? "";
        Address = station.Address ?? "";
    }

}