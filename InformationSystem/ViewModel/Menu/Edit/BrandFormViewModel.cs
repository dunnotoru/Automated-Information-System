using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class BrandFormViewModel : EditViewModel
{
    private readonly Brand _brand;

    protected override int? Save(DomainContext context)
    {
        context.Brands.Update(_brand);
        context.SaveChanges();
        return _brand.Id;
    }

    protected override void Remove(DomainContext context)
    {
        context.Remove(_brand);
        context.SaveChanges();
    }

    protected override bool CanSave() => !string.IsNullOrWhiteSpace(Name);

    public BrandFormViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _brand = new Brand();
    }

    public BrandFormViewModel(Brand brand, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _brand = brand;
        Id = _brand.Id;
    }
    
    public string Name
    {
        get => _brand.Name;
        set { _brand.Name = value; RaisePropertyChanged(); }
    }
}