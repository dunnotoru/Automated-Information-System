using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class CategoryEditViewModel : EditViewModel
{
    private string _name = string.Empty;
    
    public override ICommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Category
        {
            Id = this.Id,
            Name = _name
        }), CanSave);
    public override ICommand RemoveCommand => new RelayCommand(ExecuteRemove<Category>);

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
        set { _name = value; NotifyPropertyChanged(); }
    }
}