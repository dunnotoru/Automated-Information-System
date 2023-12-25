using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Windows.Input;
using System;
using UI.Command;
using UI.Services;
using System.Collections.ObjectModel;
using UI.ViewModel.HelperViewModels;
using UI.View;

namespace UI.ViewModel.Books.EditViewModels
{
    internal class VehicleModelEditViewModel : ViewModelBase
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IVehicleModelRepository _vehicleModelRepository;
        
        private int _id;
        private string _name;
        private int _capacity;
        private BrandViewModel _selectedBrand;
        private ObservableCollection<BrandViewModel> _brands;

        public event EventHandler Save;
        public event EventHandler Remove;
        public event EventHandler<Exception> Error;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public VehicleModelEditViewModel(VehicleModel vehicleModel, IBrandRepository brandRepository, IVehicleModelRepository vehicleModelRepository) 
            : this(brandRepository, vehicleModelRepository)
        {
            ArgumentNullException.ThrowIfNull(vehicleModel);
            
            _id = vehicleModel.Id;
            _name = vehicleModel.Name;
            _capacity = vehicleModel.Capacity;
            _selectedBrand = new BrandViewModel(_brandRepository.GetById(vehicleModel.BrandId));

            foreach (Brand item in _brandRepository.GetAll())
            {
                BrandViewModel vm = new BrandViewModel(item);
                Brands.Add(vm);
            }
        }
        public VehicleModelEditViewModel(IBrandRepository brandRepository, IVehicleModelRepository vehicleModelRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(brandRepository);
            ArgumentNullException.ThrowIfNull(vehicleModelRepository);

            _brandRepository = brandRepository;
            _vehicleModelRepository = vehicleModelRepository;

            _id = 0;
            _name = "";
            _capacity = 0;
            _selectedBrand = null;

            SaveCommand = new RelayCommand(ExecuteSave, CanSave);
            RemoveCommand = new RelayCommand(ExecuteRemove);
            Brands = new ObservableCollection<BrandViewModel>();
        }
        
        private bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name) &&  SelectedBrand != null;
        }

        private void ExecuteSave()
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
                Error?.Invoke(this, e); return;
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
                _brandRepository.Remove(Id);
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
