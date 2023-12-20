using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel
{
    internal class RunMenuViewModel : ViewModelBase
    {
        private readonly IRunRepository _runRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IMessageBoxService _messageBoxService;

        private RunEditViewModel _selectedRun;

        public ObservableCollection<RunEditViewModel> Runs { get; set; }

        public RunEditViewModel SelectedRun
        {
            get => _selectedRun;
            set { _selectedRun = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
        }

        public bool IsRedactingEnabled => SelectedRun != null;

        public ICommand AddCommand { get; }

        public RunMenuViewModel(IRunRepository runRepository, IRouteRepository routeRepository,
            IVehicleRepository vehicleRepository, IMessageBoxService messageBoxService, IDriverRepository driverRepository)
        {
            ArgumentNullException.ThrowIfNull(runRepository);
            ArgumentNullException.ThrowIfNull(routeRepository);
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(driverRepository);

            _runRepository = runRepository;
            _routeRepository = routeRepository;
            _vehicleRepository = vehicleRepository;
            _messageBoxService = messageBoxService;
            _driverRepository = driverRepository;

            Runs = new ObservableCollection<RunEditViewModel>();
            foreach (var item in _runRepository.GetAll())
            {
                RunEditViewModel vm = new RunEditViewModel(item, _runRepository,
                    _routeRepository, _vehicleRepository, _driverRepository);
                Runs.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void Add()
        {
            RunEditViewModel vm = new RunEditViewModel(_runRepository,
                    _routeRepository, _vehicleRepository, _driverRepository);
            vm.RemoveEvent += OnRemove;
            vm.ErrorEvent += OnError;
            Runs.Add(vm);
            SelectedRun = vm;
        }

        private void OnRemove(RunEditViewModel vm)
        {
            vm.RemoveEvent -= OnRemove;
            vm.ErrorEvent -= OnError;
            if (Runs.Remove(vm))
            {
                _messageBoxService.ShowMessage("Рейс удалён");
            }
        }

        private void OnError(string message)
        {
            _messageBoxService.ShowMessage($"Ошибка: {message}");
        }
    }
}
