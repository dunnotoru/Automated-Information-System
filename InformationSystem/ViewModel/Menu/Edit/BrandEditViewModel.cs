using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class BrandEditViewModel : EditViewModel
{
    private string _name = string.Empty;
    
    public override IRelayCommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Brand
        {
            Id = this.Id,
            Name = _name
        }), CanSave);
    
    public override IRelayCommand RemoveCommand => new RelayCommand(ExecuteRemove<Brand>);
    
    protected override bool CanSave() => !string.IsNullOrWhiteSpace(_name);

    public BrandEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }
    
    public BrandEditViewModel(Brand brand, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    { 
        Id = brand.Id;
        _name = brand.Name;
    }

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
}