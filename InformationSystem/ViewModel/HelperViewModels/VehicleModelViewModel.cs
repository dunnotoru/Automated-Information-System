using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class VehicleModelViewModel : ViewModelBase
{
    private readonly VehicleModel _vehicleModel;

    public VehicleModelViewModel(VehicleModel vehicleModel)
    {
        _vehicleModel = vehicleModel;
    }

    public int Id => _vehicleModel.Id;

    public string Name => _vehicleModel.Name;

    public int Capacity => _vehicleModel.Capacity;

    public string BrandName => _vehicleModel.Brand.Name;
}