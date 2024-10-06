using System;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class CategoryEditViewModel : EditViewModel
{
    private string _name = string.Empty;

    public CategoryEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }
    
    public CategoryEditViewModel(Category category, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = category.Id;
        Name = category.Name;
    }
    
    protected override bool CanSave() => !string.IsNullOrWhiteSpace(Name);

    protected override void ExecuteSave()
    {
        throw new NotImplementedException();
    }

    protected override void ExecuteRemove()
    {
        throw new NotImplementedException();
    }
    
    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }
}