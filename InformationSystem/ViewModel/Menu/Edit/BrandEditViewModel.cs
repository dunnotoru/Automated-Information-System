using System.Windows;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class BrandEditViewModel : EditViewModel
{
    private string _name = string.Empty;
    
    public override ICommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Brand
        {
            Id = this.Id,
            Name = _name
        }), CanSave);
    
    public override ICommand RemoveCommand => new RelayCommand(ExecuteRemove<Brand>);
    
    protected override bool CanSave() => !string.IsNullOrWhiteSpace(Name);

    public BrandEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }

    public BrandEditViewModel(Brand brand, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = brand.Id;
        _name = brand.Name;
    }

    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }
}