using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using InformationSystem.Command;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class VehicleModelEditViewModel : EditViewModel
{
    private ObservableCollection<BrandViewModel> _brandViewModels = new ObservableCollection<BrandViewModel>();
    private BrandViewModel? _selectedBrand = null;
    private string _name = string.Empty;
    private int _capacity = 0;
    private int _brandId = 0;

    public override ICommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() =>
        {
            return new VehicleModel
            {
                Id = this.Id,
                Name = _name,
                Capacity = _capacity,
                BrandId = _selectedBrand.Id
            };
        }), CanSave);
    
    public override ICommand RemoveCommand => new RelayCommand(ExecuteRemove<VehicleModel>);

    public VehicleModelEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        DomainContext context = contextFactory.CreateDbContext();
        _brandViewModels = new ObservableCollection<BrandViewModel>(context.Brands.Select(b => new BrandViewModel(b)));
        _selectedBrand = _brandViewModels.FirstOrDefault();
    }
    
    public VehicleModelEditViewModel(VehicleModel vehicleModel, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = vehicleModel.Id;
        _name = vehicleModel.Name;
        _capacity = vehicleModel.Capacity;
        _brandId = vehicleModel.BrandId;
        
        DomainContext context = contextFactory.CreateDbContext();
        _brandViewModels = new ObservableCollection<BrandViewModel>(context.Brands.Select(b => new BrandViewModel(b)));
        _selectedBrand = _brandViewModels.FirstOrDefault(b => b.Id == _brandId);
    }

    protected override bool CanSave()
    {
        return _selectedBrand is not null;
    }

    public ObservableCollection<BrandViewModel> BrandViewModels
    {
        get => _brandViewModels;
        set { _brandViewModels = value; NotifyPropertyChanged(); }
    }

    public BrandViewModel? SelectedBrand
    {
        get => _selectedBrand;
        set { _selectedBrand = value; NotifyPropertyChanged(); }
    }
    
    public string Name
    {
        get => _name;
        set { _name = value; NotifyPropertyChanged();}
    }

    public int Capacity
    {
        get => _capacity;
        set { _capacity = value; NotifyPropertyChanged();}
    }

    public int BrandId
    {
        get => _brandId;
        set => _brandId = value;
    }
}