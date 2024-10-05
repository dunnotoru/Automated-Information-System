using Domain.Models;
using System;

namespace UI.ViewModel.HelperViewModels;

internal class BrandViewModel : ViewModelBase
{
    private string _name;

    public BrandViewModel(Brand brand)
    {
        ArgumentNullException.ThrowIfNull(brand);
        Id = brand.Id;
        Name = brand.Name;
    }

    public int Id { get; }

    public string Name
    {
        get { return _name; }
        set { _name = value; OnPropertyChanged(); }
    }
}