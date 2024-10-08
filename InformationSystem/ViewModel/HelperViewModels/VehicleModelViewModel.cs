using CommunityToolkit.Mvvm.ComponentModel;
using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class VehicleModelViewModel : ObservableObject
{
    private string _name;
    private int _capacity;
    private string _brandName;

    public VehicleModelViewModel(VehicleModel vehicleModel)
    {
        Id = vehicleModel.Id;
        _name = vehicleModel.Name;
        _capacity = vehicleModel.Capacity;
        _brandName = vehicleModel.Brand.Name;
    }

    public int Id { get; }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public int Capacity
    {
        get => _capacity;
        set { _capacity = value; OnPropertyChanged(); }
    }

    public string BrandName
    {
        get => _brandName;
        set { _brandName = value; OnPropertyChanged(); }
    }
}