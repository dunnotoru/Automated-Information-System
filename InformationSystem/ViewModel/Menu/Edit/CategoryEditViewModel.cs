using System;
using System.Linq;
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
        DomainContext context = ContextFactory.CreateDbContext();

        try
        {
            Category category = new Category()
            {
                Id = this.Id,
                Name = this.Name
            };
            context.Categories.Add(category);
            context.SaveChanges();
            Id = category.Id;
            RaiseSaved();
        }
        catch (Exception ex)
        {
            RaiseOnErrorOccured(ex);
        }
        finally
        {
            context.Dispose();
        }
    }

    protected override void ExecuteRemove()
    {
        if (Id == 0)
        {
            return;
        }
        
        DomainContext context = ContextFactory.CreateDbContext();
        try
        {
            context.Categories
                .Where(o => o.Id == Id)
                .ExecuteDelete();
            context.SaveChanges();
            RaiseRemoved();
        }
        catch (Exception ex)
        {
            RaiseOnErrorOccured(ex);
        }
        finally
        {
            context.Dispose();
        }
    }
    
    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }
}