using System;
using System.Linq;
using InformationSystem.Data.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Books.EditViewModels;

internal sealed class BrandEditViewModel : EditViewModel
{
    private string _name = string.Empty;

    public BrandEditViewModel(Brand brand, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = brand.Id;
        Name = brand.Name;
    }

    public BrandEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }

    protected override bool CanSave() => !string.IsNullOrWhiteSpace(Name);

    protected override void ExecuteSave()
    {
        DomainContext context = ContextFactory.CreateDbContext();

        try
        {
            Brand brand = new Brand()
            {
                Id = this.Id,
                Name = this.Name,
            };
            context.Brands.Update(brand);
            context.SaveChanges();
            Id = brand.Id;
            OnSave();
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
        finally
        {
            context.Dispose();
        }
    }
    
    protected override void ExecuteRemove()
    {
        if (Id == 0) return;

        DomainContext context = ContextFactory.CreateDbContext();
        try
        {
            Brand storedEntity = context.Brands.First(o => o.Id == this.Id);
            context.Brands.Remove(storedEntity);
            context.SaveChanges();
            OnSave();
        }
        catch (Exception ex)
        {
            OnError(ex);
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