using System.Collections.ObjectModel;
using System.Linq;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class VehicleModelFormViewModel : EditViewModel
{
    private ObservableCollection<BrandViewModel> _brandViewModels;
    private BrandViewModel? _selectedBrand;
    private readonly VehicleModel _vehicleModel;
    
    protected override int? Save(DomainContext context)
    {
        _vehicleModel.BrandId = _selectedBrand!.Id;
        context.VehicleModels.Update(_vehicleModel);
        context.SaveChanges();
        context.Entry(_vehicleModel).Reload();
        return _vehicleModel.Id;
    }

    protected override void Remove(DomainContext context)
    {
        context.VehicleModels.Remove(_vehicleModel);
        context.SaveChanges();
    }
    
    public VehicleModelFormViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _vehicleModel = new VehicleModel();
        
        DomainContext context = contextFactory.CreateDbContext();
        _brandViewModels = new ObservableCollection<BrandViewModel>(context.Brands.Select(b => new BrandViewModel(b)));
        _selectedBrand = _brandViewModels.FirstOrDefault();
    }
    
    public VehicleModelFormViewModel(VehicleModel vehicleModel, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _vehicleModel = vehicleModel;
        Id = _vehicleModel.Id;
        
        DomainContext context = contextFactory.CreateDbContext();
        _brandViewModels = new ObservableCollection<BrandViewModel>(
            context.Brands.Select(b => new BrandViewModel(b)));
        _selectedBrand = _brandViewModels.FirstOrDefault(b => b.Id == _vehicleModel.BrandId);
    }

    protected override bool CanSave()
    {
        return _selectedBrand is not null;
    }

    public ObservableCollection<BrandViewModel> BrandViewModels
    {
        get => _brandViewModels;
        set { _brandViewModels = value; RaisePropertyChanged(); }
    }

    public BrandViewModel? SelectedBrand
    {
        get => _selectedBrand;
        set { _selectedBrand = value; RaisePropertyChanged(); }
    }
    
    public string Name
    {
        get => _vehicleModel.Name;
        set { _vehicleModel.Name = value; RaisePropertyChanged();}
    }

    public int Capacity
    {
        get => _vehicleModel.Capacity;
        set { _vehicleModel.Capacity = value; RaisePropertyChanged();}
    }
}