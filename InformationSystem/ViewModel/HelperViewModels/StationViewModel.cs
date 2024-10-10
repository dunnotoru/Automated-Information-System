using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class StationViewModel : ViewModelBase
{
    private readonly Station _station;

    public StationViewModel(Station station)
    {
        _station = station;
    }

    public int Id => _station.Id;
    public string Name => _station.Name;
    public string Address => _station.Address;
}