using Domain.Models;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel
{
    internal class StationManagerViewModel : ViewModelBase
    {
        private readonly IStationRepository _stationRepository;
        private readonly IMessageBoxService _messageBoxService;
        private Station _selectedStation;
        private State _currentState;

        public ObservableCollection<Station> Stations { get; set; }

        public Station SelectedStation
        {
            get => _selectedStation;
            set { _selectedStation = value; NotifyPropertyChangedByCallerName(); }
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

        public ICommand AddCommand {  get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DenyCommand { get; }

        public StationManagerViewModel(IStationRepository stationRepository, 
            IMessageBoxService messageBoxService)
        {
            ArgumentNullException.ThrowIfNull(stationRepository);
            _stationRepository = stationRepository;
            _messageBoxService = messageBoxService;

            CurrentState = State.None;

            try
            {
                Stations = new ObservableCollection<Station>(_stationRepository.GetAll());
            }
            catch (DbUpdateException e)
            {
                _messageBoxService.ShowMessage(e.Message + "Попробуйте перезагрузить страницу");
            }

            AddCommand = new RelayCommand(Add, () => CurrentState == State.None);
            DeleteCommand = new RelayCommand(Delete, () => CurrentState == State.None && SelectedStation != null);
            EditCommand = new RelayCommand(Edit, () => CurrentState == State.None && SelectedStation != null);
            SaveCommand = new RelayCommand(Save, () => CurrentState != State.None);
            DenyCommand = new RelayCommand(Deny, () => CurrentState != State.None);
        }

        private void Add()
        {
            CurrentState = State.Add;
            SelectedStation = new Station();
        }

        private void Delete()
        {
            if (!Stations.Contains(SelectedStation)) return;

            try
            {
                _stationRepository.Remove(SelectedStation);
                Stations.Remove(SelectedStation);
            }
            catch(DbUpdateException e)
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
            try
            {
                if(CurrentState == State.Add)
                {
                    _stationRepository.Add(SelectedStation);
                    Stations.Add(SelectedStation);
                }
                else if(CurrentState == State.Edit)
                {
                    _stationRepository.Update(SelectedStation);
                }
            }
            catch (DbUpdateException e)
            {
                _messageBoxService.ShowMessage(e.Message);
            }

            CurrentState = State.None;
        }

        private void Deny()
        {
            CurrentState = State.None;
        }
    }
}
