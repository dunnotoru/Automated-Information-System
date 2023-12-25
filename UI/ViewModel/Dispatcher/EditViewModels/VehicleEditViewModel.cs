using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;
using System.Windows.Input;
using UI.Command;
using UI.ViewModel.HelperViewModels;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    internal class VehicleEditViewModel : ViewModelBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IVehicleModelRepository _vehicleModelRepository;
        private readonly IRepairTypeRepository _repairTypeRepository;
        private readonly IFreighterRepository _freighterRepository;

        private int _id;
        private string _licensePlateNumber;
        private DateTime _manufacture;
        private DateTime _lastRepair;
        private int _mileage;
        private string _photography;
        private string _insuranceDetails;
        private RepairTypeViewModel _selectedRepairType;
        private FreighterViewModel __selectedFreighter;
        private VehicleModelViewModel _selectedVehicleModel;

        private ObservableCollection<VehicleModelViewModel> _vehicleModels;
        private ObservableCollection<RepairTypeViewModel> _repairTypes;
        private ObservableCollection<FreighterViewModel> _freighters;

        public EventHandler Save;
        public EventHandler Remove;
        public EventHandler<Exception> Error;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public VehicleEditViewModel(Vehicle vehicle, IVehicleRepository vehicleRepository,
            IVehicleModelRepository vehicleModelRepository, IRepairTypeRepository repairTypeRepository, IFreighterRepository freighterRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            ArgumentNullException.ThrowIfNull(vehicleRepository);

            Id = vehicle.Id;
            LicensePlateNumber = vehicle.LicensePlateNumber;
            Manufacture = vehicle.Manufacture;
            LastRepair = vehicle.LastRepair;
            Mileage = vehicle.Mileage;
            Photography = vehicle.Photography ?? "";
            InsuranceDetails = vehicle.InsuranceDetails;
            SelectedVehicleModel = new VehicleModelViewModel(vehicle.VehicleModel);
            SelectedFreighter = new FreighterViewModel(vehicle.Freighter);
            SelectedRepairType = new RepairTypeViewModel(vehicle.RepairType);

            _vehicleRepository = vehicleRepository;
            _vehicleModelRepository = vehicleModelRepository;
            _repairTypeRepository = repairTypeRepository;
            _freighterRepository = freighterRepository;

            Init();
        }

        public VehicleEditViewModel(IVehicleRepository vehicleRepository,
            IVehicleModelRepository vehicleModelRepository, IRepairTypeRepository repairTypeRepository, IFreighterRepository freighterRepository) : this()
        {
            Id = 0;
            LicensePlateNumber = "";

            Manufacture = DateTime.Now;
            LastRepair = DateTime.Now;
            Mileage = 0;
            Photography = "";
            InsuranceDetails = "";
            SelectedFreighter = null;
            SelectedVehicleModel = null;
            SelectedRepairType = null;

            _vehicleRepository = vehicleRepository;
            _vehicleModelRepository = vehicleModelRepository;
            _repairTypeRepository = repairTypeRepository;
            _freighterRepository = freighterRepository;

            Init();
        }

        private VehicleEditViewModel()
        {
            SaveCommand = new RelayCommand(ExecuteSave, CanSave);
            RemoveCommand = new RelayCommand(ExecuteRemove);

            Freighters = new ObservableCollection<FreighterViewModel>();
            RepairTypes = new ObservableCollection<RepairTypeViewModel>();
            VehicleModels = new ObservableCollection<VehicleModelViewModel>();
        }

        private void Init()
        {
            foreach(var item in _freighterRepository.GetAll())
                Freighters.Add(new FreighterViewModel(item));

            foreach (var item in _repairTypeRepository.GetAll())
                RepairTypes.Add(new RepairTypeViewModel(item));

            foreach (var item in _vehicleModelRepository.GetAll())
                VehicleModels.Add(new VehicleModelViewModel(item));
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(LicensePlateNumber) &&
                SelectedFreighter != null &&
                SelectedVehicleModel != null &&
                SelectedRepairType != null &&
                !string.IsNullOrWhiteSpace(InsuranceDetails) &&
                Mileage >= 0;
        }
        private void ExecuteSave()
        {
            Vehicle vehicle = new Vehicle()
            {
                InsuranceDetails = InsuranceDetails,
                LastRepair = LastRepair,
                LicensePlateNumber = LicensePlateNumber,
                Manufacture = Manufacture,
                Mileage = Mileage,
                Photography = Photography,
                Freighter = _freighterRepository.GetById(SelectedFreighter.Id),
                VehicleModel = _vehicleModelRepository.GetById(SelectedVehicleModel.Id),
                RepairType = _repairTypeRepository.GetById(SelectedRepairType.Id)
            };
            try
            {
                if (Id == 0)
                {
                    _vehicleRepository.Create(vehicle);
                }
                else
                {
                    _vehicleRepository.Update(Id, vehicle);
                }
                Save?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Error?.Invoke(this, e);
            }
        }

        private void ExecuteRemove()
        {
            if (Id == 0) return;
            try
            {
                _vehicleRepository.Remove(Id);
                Remove?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Error?.Invoke(this, e);
            }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }
        public string LicensePlateNumber
        {
            get { return _licensePlateNumber; }
            set { _licensePlateNumber = value; OnPropertyChanged(); }
        }
        public DateTime Manufacture
        {
            get { return _manufacture; }
            set { _manufacture = value; OnPropertyChanged(); }
        }
        public DateTime LastRepair
        {
            get { return _lastRepair; }
            set { _lastRepair = value; OnPropertyChanged(); }
        }
        public RepairTypeViewModel SelectedRepairType
        {
            get { return _selectedRepairType; }
            set { _selectedRepairType = value; OnPropertyChanged(); }
        }
        public int Mileage
        {
            get { return _mileage; }
            set { _mileage = value; OnPropertyChanged(); }
        }
        public string Photography
        {
            get { return _photography; }
            set { _photography = value; OnPropertyChanged(); }
        }
        public FreighterViewModel SelectedFreighter
        {
            get { return __selectedFreighter; }
            set { __selectedFreighter = value; OnPropertyChanged(); }
        }
        public VehicleModelViewModel SelectedVehicleModel
        {
            get { return _selectedVehicleModel; }
            set { _selectedVehicleModel = value; OnPropertyChanged(); }
        }
        public string InsuranceDetails
        {
            get { return _insuranceDetails; }
            set { _insuranceDetails = value; OnPropertyChanged(); }
        }
        public ObservableCollection<VehicleModelViewModel> VehicleModels
        {
            get { return _vehicleModels; }
            set { _vehicleModels = value; OnPropertyChanged(); }
        }
        public ObservableCollection<RepairTypeViewModel> RepairTypes
        {
            get { return _repairTypes; }
            set { _repairTypes = value; OnPropertyChanged(); }
        }
        public ObservableCollection<FreighterViewModel> Freighters
        {
            get { return _freighters; }
            set { _freighters = value; OnPropertyChanged(); }
        }
    }
}
