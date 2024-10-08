using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class BrandViewModel : ViewModelBase
{
    private string _name;

    public BrandViewModel(Brand brand)
    {
        Id = brand.Id;
        _name = brand.Name;
    }

    public int Id { get; }

    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }
}