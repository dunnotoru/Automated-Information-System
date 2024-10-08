using CommunityToolkit.Mvvm.ComponentModel;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.HelperViewModels;

public class CategoryViewModel : ObservableObject
{
    private readonly IDbContextFactory<DomainContext> _contextFactory;
    private string _name = string.Empty;
    private bool _isSelected = false;

    public CategoryViewModel(Category category, IDbContextFactory<DomainContext> contextFactory)
    {
        _contextFactory = contextFactory;
        Id = category.Id;
        _name = category.Name;
    }
    public int Id { get; }
    
    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public bool IsSelected
    {
        get => _isSelected;
        set { _isSelected = value; OnPropertyChanged(); }
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