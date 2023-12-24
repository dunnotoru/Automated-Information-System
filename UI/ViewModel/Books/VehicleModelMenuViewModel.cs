using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using UI.Command;
using UI.Services;
using UI.ViewModel.Books.BookEditViewModels;

namespace UI.ViewModel
{
    internal class VehicleModelMenuViewModel : ViewModelBase
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IVehicleModelRepository _vehicleModelRepository;
        private readonly IBrandRepository _brandRepository;

        private ObservableCollection<VehicleModelEditViewModel> _items;
        private VehicleModelEditViewModel _selectedItem;

        public ICommand AddCommand { get; }

        public VehicleModelMenuViewModel(IMessageBoxService messageBoxService, IBrandRepository brandRepository, IVehicleModelRepository vehicleModelRepository)
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(brandRepository);
            ArgumentNullException.ThrowIfNull(vehicleModelRepository);

            _messageBoxService = messageBoxService;
            _brandRepository = brandRepository;
            _vehicleModelRepository = vehicleModelRepository;

            Items = new ObservableCollection<VehicleModelEditViewModel>();
            foreach (VehicleModel item in _vehicleModelRepository.GetAll())
            {
                VehicleModelEditViewModel vm = new VehicleModelEditViewModel(item.Id,
                    item.Name, item.Capacity, item.BrandId, _messageBoxService, _brandRepository, _vehicleModelRepository);
                vm.RemoveEvent += OnRemove;
                Items.Add(vm);
            }

            AddCommand = new RelayCommand(Add);
        }

        private void OnRemove(VehicleModelEditViewModel model)
        {
            model.RemoveEvent -= OnRemove;
            Items.Remove(model);
        }

        private void Add()
        {
            VehicleModelEditViewModel vm = new VehicleModelEditViewModel(_messageBoxService, _brandRepository, _vehicleModelRepository);
            vm.RemoveEvent += OnRemove;
            Items.Add(vm);
            SelectedItem = vm;
        }

        public bool IsRedactingEnabled => SelectedItem != null;

        public ObservableCollection<VehicleModelEditViewModel> Items
        {
            get { return _items; }
            set { _items = value; OnPropertyChanged(); }
        }

        public VehicleModelEditViewModel SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(); OnPropertyChangedByName(nameof(IsRedactingEnabled)); }
        }
    }
}
