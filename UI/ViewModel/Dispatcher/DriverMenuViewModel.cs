using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;
using UI.ViewModel.Dispatcher.EditViewModels;

namespace UI.ViewModel
{
    internal class DriverMenuViewModel : ViewModelBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IMessageBoxService _messageBoxService;
        private DriverEditViewModel _selectedDriver;
        private ObservableCollection<DriverEditViewModel> _drivers;

        public ObservableCollection<DriverEditViewModel> Drivers
        {
            get { return _drivers; }
            set { _drivers = value; OnPropertyChanged(); }
        }

        public DriverEditViewModel SelectedDriver
        {
            get => _selectedDriver;
            set { _selectedDriver = value; OnPropertyChanged(); }
        }

        public bool IsRedactingEnabled => SelectedDriver != null;

        public ICommand AddCommand { get; }

        public DriverMenuViewModel(IDriverRepository driverRepository, IMessageBoxService messageBoxService, 
            ICategoryRepository categoryRepository)
        {
            _driverRepository = driverRepository;
            _messageBoxService = messageBoxService;
            _categoryRepository = categoryRepository;

            Drivers = new ObservableCollection<DriverEditViewModel>();
            foreach (Driver item in _driverRepository.GetAll())
            {
                DriverEditViewModel vm = new DriverEditViewModel(item, _driverRepository, _categoryRepository);
                vm.RemoveEvent += OnRemove;
                vm.ErrorEvent += OnError;
                Drivers.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void Add()
        {
            DriverEditViewModel vm = new DriverEditViewModel(_driverRepository, _categoryRepository);
            vm.RemoveEvent += OnRemove;
            vm.ErrorEvent += OnError;
            Drivers.Add(vm);
            SelectedDriver = vm;
        }

        private void OnRemove(DriverEditViewModel vm)
        {
            vm.RemoveEvent -= OnRemove;
            vm.ErrorEvent -= OnError;
            if (Drivers.Remove(vm))
            {
                _messageBoxService.ShowMessage("Водитель удалён");
            }
        }

        private void OnError(string message)
        {
            _messageBoxService.ShowMessage($"Ошибка: {message}");
        }
    }
}
