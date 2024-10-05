using Domain.Models;
using System;

namespace UI.ViewModel.HelperViewModels;

internal class VehicleModelViewModel : ViewModelBase
{
    private int _id;
    private string _name;
    private int _capacity;
    private string _brandName;

    public VehicleModelViewModel(VehicleModel vehicleModel)
    {
        ArgumentNullException.ThrowIfNull(vehicleModel);

        Id = vehicleModel.Id;
        Name = vehicleModel.Name;
        Capacity = vehicleModel.Capacity;
        BrandName = vehicleModel.Brand.Name;
    }

    public int Id
    {
        get { return _id; }
        set { _id = value; OnPropertyChanged(); }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; OnPropertyChanged(); }
    }

    public int Capacity
    {
        get { return _capacity; }
        set { _capacity = value; OnPropertyChanged(); }
    }

    public string BrandName
    {
        get { return _brandName; }
        set { _brandName = value; OnPropertyChanged(); }
    }
}