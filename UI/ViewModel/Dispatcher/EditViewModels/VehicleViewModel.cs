using Domain.Models;
using Domain.RepositoryInterfaces;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    public class VehicleViewModel : ViewModelBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private int _id;
        private string _licensePlateNumber;

        public VehicleViewModel(Vehicle vehicle, IVehicleRepository vehicleRepository)
        {
            Id = vehicle.Id;
            LicensePlateNumber = vehicle.LicensePlateNumber;
            _vehicleRepository = vehicleRepository;
        }

        public VehicleViewModel(IVehicleRepository vehicleRepository)
        {
            Id = 0;
            LicensePlateNumber = "";
            _vehicleRepository = vehicleRepository;
        }

        public string LicensePlateNumber
        {
            get { return _licensePlateNumber; }
            set { _licensePlateNumber = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        public Vehicle GetVehicle()
        {
            return _vehicleRepository.GetById(Id);
        }
    }
}