using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.HelperViewModels;

public class CategoryViewModel : ViewModelBase
{
    private readonly IDbContextFactory<DomainContext> _contextFactory;
    private string _name = string.Empty;
    private bool _isSelected = false;

    public CategoryViewModel(Category category, IDbContextFactory<DomainContext> contextFactory)
    {
        _contextFactory = contextFactory;
        Id = category.Id;
        Name = category.Name;
    }
    public int Id { get; }
    
    public string Name
    {
        get { return _name; }
        set { _name = value; NotifyPropertyChanged(); }
    }

    public bool IsSelected
    {
        get { return _isSelected; }
        set { _isSelected = value; NotifyPropertyChanged(); }
    }

    public Category? GetCategory()
    {
        Category? category = null;
        using (DomainContext context = _contextFactory.CreateDbContext())
        {
            category = context.Categories.Find(Id);
        }
        return category;
    }
}