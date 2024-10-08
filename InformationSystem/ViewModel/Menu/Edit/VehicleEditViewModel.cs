using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using InformationSystem.Domain.Context;
using InformationSystem.Domain.Models;
using InformationSystem.ViewModel.HelperViewModels;
using InformationSystem.ViewModel.Sales;
using Microsoft.EntityFrameworkCore;

namespace InformationSystem.ViewModel.Menu.Edit;

public class VehicleEditViewModel : EditViewModel
{
    private string _licensePlateNumber = string.Empty;
    private DateTime _manufacture = DateTime.Now;
    private DateTime _lastRepair = DateTime.Now;
    private int _mileage = 0;
    private string? _photo = null;
    private string _insuranceDetails = string.Empty;
    
    private ObservableCollection<FreighterViewModel> _freighterItems;
    private ObservableCollection<RepairTypeViewModel> _repairTypeItems;
    private ObservableCollection<RunViewModel> _runItems;
    private ObservableCollection<VehicleModelViewModel> _vehicleModelItems;
    
    private FreighterViewModel? _selectedFreighter;
    private RepairTypeViewModel? _selectedRepairType;
    private RunViewModel? _selectedRun;
    private VehicleModelViewModel? _selectedVehicleModel;
    
    public override IRelayCommand SaveCommand => new RelayCommand(() => 
        ExecuteSave(() => new Vehicle
        {
            Id = this.Id,
            FreighterId = _selectedFreighter.Id,
            InsuranceDetails = _insuranceDetails,
            LastRepair = _lastRepair,
            LicensePlateNumber = _licensePlateNumber,
            Manufacture = _manufacture,
            Mileage = _mileage,
            Photography = _photo,
            RepairTypeId = _selectedRepairType?.Id,
            RunId = _selectedRun?.Id,
            VehicleModelId = _selectedVehicleModel.Id,
        }), CanSave);
    
    public override IRelayCommand RemoveCommand => new RelayCommand(ExecuteRemove<Vehicle>);
    
    public VehicleEditViewModel(Vehicle vehicle, IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        Id = vehicle.Id;
        _insuranceDetails = vehicle.InsuranceDetails;
        _lastRepair = vehicle.LastRepair;
        _manufacture = vehicle.Manufacture;
        _licensePlateNumber = vehicle.LicensePlateNumber;
        _mileage = vehicle.Mileage;
        _photo = vehicle.Photography;
        
        DomainContext context = contextFactory.CreateDbContext();
        _freighterItems = new ObservableCollection<FreighterViewModel>(
            context.Freighters.Select(f => new FreighterViewModel(f)));
        _runItems = new ObservableCollection<RunViewModel>(
            context.Runs.Select(r => new RunViewModel(r, 0))); //TODO: why 000
        _repairTypeItems = new ObservableCollection<RepairTypeViewModel>(
            context.RepairTypes.Select(r => new RepairTypeViewModel(r)));
        _vehicleModelItems = new ObservableCollection<VehicleModelViewModel>(
            context.VehicleModels.Include(v => v.Brand).Select(v => new VehicleModelViewModel(v)));
        
        _selectedFreighter = _freighterItems.FirstOrDefault(f => f.Id == vehicle.FreighterId);
        _selectedRun = _runItems.FirstOrDefault(r => r.Id == vehicle.RunId);
        _selectedRepairType = _repairTypeItems.FirstOrDefault(r => r.Id == vehicle.RepairTypeId);
        _selectedVehicleModel = _vehicleModelItems.FirstOrDefault(v => v.Id == vehicle.VehicleModelId);
    }
    
    public VehicleEditViewModel(IDbContextFactory<DomainContext> contextFactory) : base(contextFactory)
    {
        DomainContext context = contextFactory.CreateDbContext();
        _freighterItems = new ObservableCollection<FreighterViewModel>(
            context.Freighters.Select(f => new FreighterViewModel(f)));
        _runItems = new ObservableCollection<RunViewModel>(
            context.Runs.Select(r => new RunViewModel(r, 0))); //TODO: why 000
        _repairTypeItems = new ObservableCollection<RepairTypeViewModel>(
            context.RepairTypes.Select(r => new RepairTypeViewModel(r)));
        _vehicleModelItems = new ObservableCollection<VehicleModelViewModel>(
            context.VehicleModels.Include(v=> v.Brand).Select(v => new VehicleModelViewModel(v)));
        
        _selectedFreighter = _freighterItems.FirstOrDefault();
        _selectedRun = _runItems.FirstOrDefault();
        _selectedRepairType = _repairTypeItems.FirstOrDefault();
        _selectedVehicleModel = _vehicleModelItems.FirstOrDefault();
    }

    protected override bool CanSave()
    {
        return _selectedRun is not null
            && _selectedVehicleModel is not null
            && _selectedFreighter is not null
            && _selectedRepairType is not null;
    }
    
    public string LicensePlateNumber
    {
        get => _licensePlateNumber;
        set => SetProperty(ref _licensePlateNumber, value);
    }

    public DateTime Manufacture
    {
        get => _manufacture;
        set => SetProperty(ref _manufacture, value);
    }

    public DateTime LastRepair
    {
        get => _lastRepair;
        set => SetProperty(ref _lastRepair, value);
    }

    public int Mileage
    {
        get => _mileage;
        set => SetProperty(ref _mileage, value);
    }

    public string? Photo
    {
        get => _photo;
        set => SetProperty(ref _photo, value);
    }

    public string InsuranceDetails
    {
        get => _insuranceDetails;
        set => SetProperty(ref _insuranceDetails, value);
    }

    public ObservableCollection<FreighterViewModel> FreighterItems
    {
        get => _freighterItems;
        set => SetProperty(ref _freighterItems, value);
    }

    public ObservableCollection<RepairTypeViewModel> RepairTypeItems
    {
        get => _repairTypeItems;
        set => SetProperty(ref _repairTypeItems, value);
    }

    public ObservableCollection<RunViewModel> RunItems
    {
        get => _runItems;
        set => SetProperty(ref _runItems, value);
    }

    public ObservableCollection<VehicleModelViewModel> VehicleModelItems
    {
        get => _vehicleModelItems;
        set => SetProperty(ref _vehicleModelItems, value);
    }

    public FreighterViewModel? SelectedFreighter
    {
        get => _selectedFreighter;
        set => SetProperty(ref _selectedFreighter, value);
    }

    public RepairTypeViewModel? SelectedRepairType
    {
        get => _selectedRepairType;
        set => SetProperty(ref _selectedRepairType, value);
    }

    public RunViewModel? SelectedRun
    {
        get => _selectedRun;
        set => SetProperty(ref _selectedRun, value);
    }

    public VehicleModelViewModel? SelectedVehicleModel
    {
        get => _selectedVehicleModel;
        set => SetProperty(ref _selectedVehicleModel, value);
    }
}