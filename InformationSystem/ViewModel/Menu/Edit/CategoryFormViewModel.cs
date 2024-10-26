using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class CategoryFormViewModel : EditViewModel
{
    private readonly Category _category;

    protected override int? Save(DomainContext context)
    {
        context.Categories.Update(_category);
        context.SaveChanges();
        return _category.Id;
    }

    protected override void Remove(DomainContext context)
    {
        context.Remove(_category);
        context.SaveChanges();
    }
    
    protected override bool CanSave() => !string.IsNullOrWhiteSpace(Name);

    public CategoryFormViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _category = new Category();
    }
    
    public CategoryFormViewModel(Category category, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _category = category;
        Id = _category.Id;
    }
    
    public string Name
    {
        get => _category.Name;
        set { _category.Name = value; RaisePropertyChanged(); }
    }
}