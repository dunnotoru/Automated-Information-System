using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel
{
    internal class VehicleManagerViewModel : ViewModelBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMessageBoxService _messageBoxService;
        private Vehicle _selectedVehicle;
        private State _currentState;

        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public Vehicle SelectedVehicle
        {
            get => _selectedVehicle;
            set { _selectedVehicle = value; NotifyPropertyChangedByCallerName(); }
        }

        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                NotifyPropertyChanged(nameof(IsRedactingEnabled));
            }
        }
        public bool IsRedactingEnabled => CurrentState != State.None;

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DenyCommand { get; }

        public VehicleManagerViewModel(IVehicleRepository vehicleRepository, IMessageBoxService messageBoxService)
        {
            _vehicleRepository = vehicleRepository;
            _messageBoxService = messageBoxService;

            try
            {
                Vehicles = new ObservableCollection<Vehicle>(_vehicleRepository.GetAll());
            }
            catch(DbUpdateException e)
            {
                _messageBoxService.ShowMessage(e.Message);
            }

            AddCommand = new RelayCommand(Add, () => CurrentState == State.None);
            DeleteCommand = new RelayCommand(Delete, () => CurrentState == State.None && SelectedVehicle != null);
            EditCommand = new RelayCommand(Edit, () => CurrentState == State.None && SelectedVehicle != null);
            SaveCommand = new RelayCommand(Save, () => CurrentState == State.Add || CurrentState == State.Edit);
            DenyCommand = new RelayCommand(Deny, () => CurrentState != State.None);
        }

        private void Add()
        {
            CurrentState = State.Add;
            SelectedVehicle = new Vehicle();
        }

        private void Delete()
        {
            if (SelectedVehicle == null)
            {
                _messageBoxService.ShowMessage("Транспорт не выбран");
                return;
            }

            try
            {
                _vehicleRepository.Remove(SelectedVehicle);
                Vehicles.Remove(SelectedVehicle);
            }
            catch (DbUpdateException e)
            {
                _messageBoxService.ShowMessage(e.Message);
            }
        }

        private void Edit()
        {
            CurrentState = State.Edit;
        }

        private void Save()
        {
            if (CurrentState == State.Add)
            {
                _vehicleRepository.Add(SelectedVehicle);
                Vehicles.Add(SelectedVehicle);
            }
            else if (CurrentState == State.Edit)
            {
                _vehicleRepository.Update(SelectedVehicle);
            }

            CurrentState = State.None;
        }

        private void Deny()
        {
            CurrentState = State.None;
        }
    }
}
