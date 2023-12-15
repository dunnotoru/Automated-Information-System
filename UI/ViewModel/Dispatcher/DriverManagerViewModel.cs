using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel
{
    internal class DriverManagerViewModel : ViewModelBase
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IMessageBoxService _messageBoxService;
        private State _currentState;
        private Driver _selectedDriver;

        public ObservableCollection<Driver> Drivers { get; set; }

        public Driver SelectedDriver
        {
            get => _selectedDriver;
            set { _selectedDriver = value; OnPropertyChanged(); }
        }

        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                OnPropertyChangedByName(nameof(IsRedactingEnabled));
            }
        }
        public bool IsRedactingEnabled => CurrentState != State.None;

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DenyCommand { get; }

        public DriverManagerViewModel(IDriverRepository driverRepository, IMessageBoxService messageBoxService)
        {
            _driverRepository = driverRepository;
            _messageBoxService = messageBoxService;

            try
            {
                Drivers = new ObservableCollection<Driver>(_driverRepository.GetAll());
            }
            catch(DbUpdateException)
            {
                _messageBoxService.ShowMessage("Произошла ошибка. Попробуйте перезагрузить страницу.");
            }

            AddCommand = new RelayCommand(Add, () => CurrentState == State.None);
            DeleteCommand = new RelayCommand(Delete, () => CurrentState == State.None && SelectedDriver != null);
            EditCommand = new RelayCommand(Edit, () => CurrentState == State.None && SelectedDriver != null);
            SaveCommand = new RelayCommand(Save, () => CurrentState == State.Add || CurrentState == State.Edit);
            DenyCommand = new RelayCommand(Deny, () => CurrentState != State.None);
        }

        private void Add()
        {
            CurrentState = State.Add;
            SelectedDriver = new Driver();
        }

        private void Delete()
        {
            if (SelectedDriver == null)
            {
                _messageBoxService.ShowMessage("Не выбран водитель.");
                return;
            }

            try
            {
                _driverRepository.Remove(SelectedDriver.Id);
                Drivers.Remove(SelectedDriver);
            }
            catch(DbUpdateException e)
            {
                _messageBoxService.ShowMessage(e.Message);
            }
        }

        private void Save()
        {
            try
            {
                if (CurrentState == State.Add)
                {
                    _driverRepository.Add(SelectedDriver);
                    Drivers.Add(SelectedDriver);
                }
                else if(CurrentState == State.Edit)
                {
                    _driverRepository.Update(SelectedDriver.Id, SelectedDriver);
                }
            }
            catch (DbUpdateException e)
            {
                _messageBoxService.ShowMessage(e.Message);
            }
            finally
            {
                CurrentState = State.None;
            }
        }

        private void Edit()
        {
            CurrentState = State.Edit;
        }

        private void Deny()
        {
            CurrentState = State.None;
        }

    }
}
