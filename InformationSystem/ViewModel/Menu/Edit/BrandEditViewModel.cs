using System;
using System.Linq;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class BrandEditViewModel : EditViewModel
{
    private string _name = string.Empty;

    public BrandEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory) { }

    public BrandEditViewModel(Brand brand, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = brand.Id;
        Name = brand.Name;
    }

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
        if (Id == 0) return;

        DomainContext context = ContextFactory.CreateDbContext();
        try
        {
            Brand storedEntity = context.Brands.First(o => o.Id == this.Id);
            context.Brands.Remove(storedEntity);
            context.SaveChanges();
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

    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged(); }
    }
}