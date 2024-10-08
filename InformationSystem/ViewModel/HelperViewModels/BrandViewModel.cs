using CommunityToolkit.Mvvm.ComponentModel;
using InformationSystem.Domain.Models;

namespace InformationSystem.ViewModel.HelperViewModels;

public class BrandViewModel : ObservableObject
{
    private string _name;

    public BrandViewModel(Brand brand)
    {
        Id = brand.Id;
        _name = brand.Name;
    }

    public int Id { get; protected init; }

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }
}