using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.Context;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class CategoryEditViewModel : EditViewModel
{
    private string _name = string.Empty;
    
    public override IRelayCommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Category
        {
            Id = this.Id,
            Name = _name
        }), CanSave);
    
    public override IRelayCommand RemoveCommand => new RelayCommand(ExecuteRemove<Category>);

    public CategoryEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }
    
    public CategoryEditViewModel(Category category, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = category.Id;
        Name = category.Name;
    }
    
    protected override bool CanSave() => !string.IsNullOrWhiteSpace(Name);

    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }
}