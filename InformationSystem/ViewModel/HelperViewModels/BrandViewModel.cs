using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class BrandViewModel : ViewModelBase
{
    private readonly Brand _brand;

    public BrandViewModel(Brand brand)
    {
        _brand = brand;
    }

    public int Id => _brand.Id;
    public string Name => _brand.Name;
}