using System;
using System.Collections.ObjectModel;
using System.Linq;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;
using InformationSystem.ViewModel.Sales;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public sealed class VehicleFormViewModel : EditViewModel
{
    private readonly Vehicle _vehicle;
    
    private ObservableCollection<FreighterViewModel> _freighterItems;
    private ObservableCollection<RepairTypeViewModel> _repairTypeItems;
    private ObservableCollection<RunViewModel> _runItems;
    private ObservableCollection<VehicleModelViewModel> _vehicleModelItems;
    
    private FreighterViewModel? _selectedFreighter;
    private RepairTypeViewModel? _selectedRepairType;
    private RunViewModel? _selectedRun;
    private VehicleModelViewModel? _selectedVehicleModel;
    
    protected override int? Save(DomainContext context)
    {
        _vehicle.FreighterId = _selectedFreighter!.Id;
        _vehicle.RunId = _selectedRun?.Id;
        _vehicle.RepairTypeId = _selectedRepairType?.Id;
        _vehicle.VehicleModelId = _selectedVehicleModel!.Id;
        context.Vehicles.Update(_vehicle);
        context.SaveChanges();
        context.Entry(_vehicle).Reload();
        return _vehicle.Id;
    }

    protected override void Remove(DomainContext context)
    {
        context.Vehicles.Remove(_vehicle);
        context.SaveChanges();
    }
    
    protected override bool CanSave()
    {
        return _selectedRepairType is not null && _selectedRun is not null; 
    }
    
    public VehicleFormViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _vehicle = new Vehicle();

        using DomainContext context = contextFactory.CreateDbContext();
        _freighterItems = new ObservableCollection<FreighterViewModel>(
            context.Freighters.Select(f => new FreighterViewModel(f)));
        _repairTypeItems = new ObservableCollection<RepairTypeViewModel>(
            context.RepairTypes.Select(r => new RepairTypeViewModel(r)));
        _runItems = new ObservableCollection<RunViewModel>(
            context.Runs.Select(r => new RunViewModel(r, 0)));
        _vehicleModelItems = new ObservableCollection<VehicleModelViewModel>(
            context.VehicleModels.Include(v => v.Brand).Select(m => new VehicleModelViewModel(m)));
        
        _selectedFreighter = _freighterItems.FirstOrDefault();
        _selectedRun = _runItems.FirstOrDefault();
        _selectedRepairType = _repairTypeItems.FirstOrDefault();
        _selectedVehicleModel = _vehicleModelItems.FirstOrDefault();
    }
    
    public VehicleFormViewModel(Vehicle vehicle, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        _vehicle = vehicle;
        Id = _vehicle.Id;
        
        using DomainContext context = contextFactory.CreateDbContext();
        _freighterItems = new ObservableCollection<FreighterViewModel>(
            context.Freighters.Select(f => new FreighterViewModel(f)));
        _repairTypeItems = new ObservableCollection<RepairTypeViewModel>(
            context.RepairTypes.Select(r => new RepairTypeViewModel(r)));
        _runItems = new ObservableCollection<RunViewModel>(
            context.Runs.Select(r => new RunViewModel(r, 0)));
        _vehicleModelItems = new ObservableCollection<VehicleModelViewModel>(
            context.VehicleModels.Select(m => new VehicleModelViewModel(m)));
        
        _selectedFreighter = _freighterItems.FirstOrDefault();
        _selectedRun = _runItems.FirstOrDefault();
        _selectedRepairType = _repairTypeItems.FirstOrDefault();
        _selectedVehicleModel = _vehicleModelItems.FirstOrDefault();
    }

    public string LicensePlateNumber
    {
        get => _vehicle.LicensePlateNumber;
        set { _vehicle.LicensePlateNumber = value; RaisePropertyChanged(); }
    }

    public DateTime Manufacture
    {
        get => _vehicle.Manufacture;
        set { _vehicle.Manufacture = value; RaisePropertyChanged(); }
    }

    public DateTime LastRepair
    {
        get => _vehicle.LastRepair;
        set { _vehicle.LastRepair = value; RaisePropertyChanged(); }
    }

    public int Mileage
    {
        get => _vehicle.Mileage;
        set { _vehicle.Mileage = value; RaisePropertyChanged(); }
    }

    public string? Photography
    {
        get => _vehicle.Photography;
        set { _vehicle.Photography = value; RaisePropertyChanged(); }
    }

    public string InsuranceDetails
    {
        get => _vehicle.InsuranceDetails;
        set { _vehicle.InsuranceDetails = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<FreighterViewModel> FreighterItems
    {
        get => _freighterItems;
        set { _freighterItems = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<RepairTypeViewModel> RepairTypeItems
    {
        get => _repairTypeItems;
        set { _repairTypeItems = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<RunViewModel> RunItems
    {
        get => _runItems;
        set { _runItems = value; RaisePropertyChanged(); }
    }

    public ObservableCollection<VehicleModelViewModel> VehicleModelItems
    {
        get => _vehicleModelItems;
        set { _vehicleModelItems = value; RaisePropertyChanged(); }
    }

    public FreighterViewModel? SelectedFreighter
    {
        get => _selectedFreighter;
        set { _selectedFreighter = value; RaisePropertyChanged(); }
    }

    public RepairTypeViewModel? SelectedRepairType
    {
        get => _selectedRepairType;
        set { _selectedRepairType = value; RaisePropertyChanged(); }
    }

    public RunViewModel? SelectedRun
    {
        get => _selectedRun;
        set { _selectedRun = value; RaisePropertyChanged(); }
    }

    public VehicleModelViewModel? SelectedVehicleModel
    {
        get => _selectedVehicleModel;
        set { _selectedVehicleModel = value; RaisePropertyChanged(); }
    }

}