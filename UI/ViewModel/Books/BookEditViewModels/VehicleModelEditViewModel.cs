using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Windows.Input;
using System;
using UI.Command;
using UI.Services;
using System.Collections.ObjectModel;

namespace UI.ViewModel.Books.BookEditViewModels
{
    internal class VehicleModelEditViewModel : ViewModelBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IVehicleModelRepository _vehicleModelRepository;
        private readonly IMessageBoxService _messageBoxService;
        private int _id;
        private string _name;
        private int _capacity;
        private BrandViewModel _selectedBrand;
        private ObservableCollection<BrandViewModel> _brands;

        public Action<VehicleModelEditViewModel> RemoveEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public VehicleModelEditViewModel(int id, string name, int capacity, int brandId, IMessageBoxService messageBoxService, IBrandRepository brandRepository, IVehicleModelRepository vehicleModelRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(vehicleModelRepository);
            _messageBoxService = messageBoxService;
            _brandRepository = brandRepository;
            _vehicleModelRepository = vehicleModelRepository;
            
            _id = id;
            _name = name;
            _capacity = capacity;
            _selectedBrand = new BrandViewModel(_brandRepository.GetById(brandId));

            LoadBrands();
        }
        public VehicleModelEditViewModel(IMessageBoxService messageBoxService, IBrandRepository brandRepository, IVehicleModelRepository vehicleModelRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(messageBoxService);
            ArgumentNullException.ThrowIfNull(brandRepository);
            ArgumentNullException.ThrowIfNull(vehicleModelRepository);

            _messageBoxService = messageBoxService;
            _brandRepository = brandRepository;
            _vehicleModelRepository = vehicleModelRepository;
            _id = 0;
            _name = "";
            _capacity = 0;
            _selectedBrand = null;

            LoadBrands();
        }
        private VehicleModelEditViewModel()
        {
            SaveCommand = new RelayCommand(Save, CanSave);
            RemoveCommand = new RelayCommand(Remove);
            Brands = new ObservableCollection<BrandViewModel>();
        }

        private void LoadBrands()
        {
            foreach (Brand item in  _brandRepository.GetAll())
            {
                BrandViewModel vm = new BrandViewModel(item);
                Brands.Add(vm);
            }
        }

        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name) &&  SelectedBrand != null;
        }

        private void Save()
        {
            VehicleModel model;
            try
            {
                model = new VehicleModel()
                {
                    Id = _id,
                    Name = Name,
                    Capacity = _capacity,
                    Brand = _brandRepository.GetById(SelectedBrand.Id),
                };
            }
            catch (Exception e)
            {
                _messageBoxService.ShowMessage($"Ошибка: {e.Message}"); return;
            }

            try
            {
                if (Id == 0)
                {
                    _vehicleModelRepository.Create(model);
                }
                else
                {
                    _vehicleModelRepository.Update(Id, model);
                }
                _messageBoxService.ShowMessage("Модель успешно сохранена.");
            }
            catch (Exception e)
            {
                _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
            }
        }
        private void Remove()
        {
            if (Id == 0) return;

            try
            {
                _brandRepository.Remove(Id);
                RemoveEvent?.Invoke(this);
            }
            catch (Exception e)
            {
                _messageBoxService.ShowMessage($"Ошибка: {e.Message}");
            }

            _messageBoxService.ShowMessage("Модель успешно удалена.");
        }

        public int Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public BrandViewModel SelectedBrand
        {
            get { return _selectedBrand; }
            set { _selectedBrand = value; OnPropertyChanged(); }
        }

        public ObservableCollection<BrandViewModel> Brands
        {
            get { return _brands; }
            set { _brands = value; OnPropertyChanged(); }
        }

        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; OnPropertyChanged(); }
        }
    }
}
