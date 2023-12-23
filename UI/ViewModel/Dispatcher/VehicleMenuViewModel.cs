using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel
{
    internal class VehicleMenuViewModel : ViewModelBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IVehicleModelRepository _vehicleModelRepository;
        private readonly IRepairTypeRepository _repairTypeRepository;
        private readonly IFreighterRepository _freighterRepository;

        private VehicleEditViewModel _selectedVehicle;

        public ObservableCollection<VehicleEditViewModel> Vehicles { get; set; }
        public VehicleEditViewModel SelectedVehicle
        {
            get => _selectedVehicle;
            set { _selectedVehicle = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
        }

        public bool IsRedactingEnabled => SelectedVehicle != null;

        public ICommand AddCommand { get; }

        public VehicleMenuViewModel(IVehicleRepository vehicleRepository, IMessageBoxService messageBoxService,
            IVehicleModelRepository vehicleModelRepository, IRepairTypeRepository repairTypeRepository, IFreighterRepository freighterRepository)
        {
            _vehicleRepository = vehicleRepository;
            _messageBoxService = messageBoxService;
            _vehicleModelRepository = vehicleModelRepository;
            _repairTypeRepository = repairTypeRepository;
            _freighterRepository = freighterRepository;

            Vehicles = new ObservableCollection<VehicleEditViewModel>();
            foreach (Vehicle item in _vehicleRepository.GetAll())
            {
                VehicleEditViewModel viewModel = new VehicleEditViewModel(item,
                                                                          _vehicleRepository,
                                                                          _vehicleModelRepository,
                                                                          _repairTypeRepository,
                                                                          _freighterRepository);
                viewModel.RemoveEvent += OnRemove;
                viewModel.ErrorEvent += OnError;
                Vehicles.Add(viewModel);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void Add()
        {
            VehicleEditViewModel viewModel = new VehicleEditViewModel(_vehicleRepository,
                                                                      _vehicleModelRepository,
                                                                      _repairTypeRepository,
                                                                      _freighterRepository);
            viewModel.RemoveEvent += OnRemove;
            viewModel.ErrorEvent += OnError;
            Vehicles.Add(viewModel);
            SelectedVehicle = viewModel;
        }

        private void OnRemove(VehicleEditViewModel viewModel)
        {
            viewModel.RemoveEvent -= OnRemove;
            viewModel.ErrorEvent -= OnError;
            if (Vehicles.Remove(viewModel))
            {
                _messageBoxService.ShowMessage("Станция удалена");
            }
        }

        private void OnError(string message)
        {
            _messageBoxService.ShowMessage($"Ошибка: {message}");
        }
    }
}
