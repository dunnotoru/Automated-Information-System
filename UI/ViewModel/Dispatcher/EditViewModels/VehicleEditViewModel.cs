using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    internal class VehicleEditViewModel : ViewModelBase
    {
        private readonly IVehicleRepository _vehicleRepository;

        private int _id;
        private string _licensePlateNumber;
        private string _model;
        private string _brand;
        private int _capacity;
        private DateTime _manufacture;
        private DateTime _lastRepair;
        private int _mileage;
        private string _photography;
        private string _freighter;
        private string _insuranceDetails;

        public Action<string> ErrorEvent;
        public Action<VehicleEditViewModel> RemoveEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public VehicleEditViewModel(Vehicle vehicle, IVehicleRepository vehicleRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            ArgumentNullException.ThrowIfNull(vehicleRepository);

            Id = vehicle.Id;
            LicensePlateNumber = vehicle.LicensePlateNumber;
            Model = vehicle.Model;
            Brand = vehicle.Brand;
            Capacity = vehicle.Capacity;
            Manufacture = vehicle.Manufacture;
            LastRepair = vehicle.LastRepair;
            Mileage = vehicle.Mileage;
            Photography = vehicle.Photography ?? "";
            Freighter = vehicle.Freighter ?? "";
            InsuranceDetails = vehicle.InsuranceDetails;

            _vehicleRepository = vehicleRepository;
        }

        public VehicleEditViewModel(IVehicleRepository vehicleRepository) : this()
        {
            Id = 0;
            LicensePlateNumber = "";
            Model = "";
            Brand = "";
            Capacity = 0;
            Manufacture = DateTime.Now;
            LastRepair = DateTime.Now;
            Mileage = 0;
            Photography = "";
            Freighter = "";
            InsuranceDetails = "";

            _vehicleRepository = vehicleRepository;
        }

        private VehicleEditViewModel()
        {
            SaveCommand = new RelayCommand(Save);
            RemoveCommand = new RelayCommand(Remove);
        }

        private void Save()
        {
            Vehicle createdStation = new Vehicle()
            {
                Brand = Brand,
                Model = Model,
                Capacity = Capacity,
                Freighter = Freighter,
                InsuranceDetails = InsuranceDetails,
                LastRepair = LastRepair,
                LastRepairType = LastRepairType,
                LicensePlateNumber = LicensePlateNumber,
                Manufacture = Manufacture,
                Mileage = Mileage,
                Photography = Photography,
            };
            try
            {
                if (Id == 0)
                {
                    _vehicleRepository.Add(createdStation);
                }
                else
                {
                    _vehicleRepository.Update(Id, createdStation);
                }
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }

        private void Remove()
        {
            if (Id == 0) return;
            try
            {
                _vehicleRepository.Remove(Id);
                RemoveEvent?.Invoke(this);
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
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

        public string Model
        {
            get { return _model; }
            set { _model = value; OnPropertyChanged(); }
        }

        public string Brand
        {
            get { return _brand; }
            set { _brand = value; OnPropertyChanged(); }
        }

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; OnPropertyChanged(); }
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
        private string _lastRepairType;

        public string LastRepairType
        {
            get { return _lastRepairType; }
            set { _lastRepairType = value; OnPropertyChanged(); }
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

        public string Freighter
        {
            get { return _freighter; }
            set { _freighter = value; OnPropertyChanged(); }
        }

        public string InsuranceDetails
        {
            get { return _insuranceDetails; }
            set { _insuranceDetails = value; OnPropertyChanged(); }
        }
    }
}
