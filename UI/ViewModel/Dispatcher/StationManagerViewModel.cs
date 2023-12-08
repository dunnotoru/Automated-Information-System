using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public class StationManagerViewModel : ViewModelBase
    {
        private readonly IStationRepository _stationRepository;
        private Station _selectedStation;
        private Station _bufferStation;
        private State _currentState;
        public ObservableCollection<Station> Stations { get; set; }
        public Station SelectedStation
        {
            get => _selectedStation;
            set { _selectedStation = value; NotifyPropertyChanged(nameof(SelectedStation)); }
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

        public StationManagerViewModel(IStationRepository stationRepository)
        {
            ArgumentNullException.ThrowIfNull(stationRepository);
            _stationRepository = stationRepository;

            CurrentState = State.None;
            Stations = new ObservableCollection<Station>(_stationRepository.GetAll());

            AddCommand = new RelayCommand(Add, () => CurrentState == State.None);
            DeleteCommand = new RelayCommand(Delete, () => CurrentState == State.None && SelectedStation != null);
            EditCommand = new RelayCommand(Edit, () => CurrentState == State.None && SelectedStation != null);
            SaveCommand = new RelayCommand(Save, () => CurrentState == State.Add || CurrentState == State.Edit);
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
            _stationRepository.Remove(SelectedStation);
            Stations.Remove(SelectedStation);
        }

        private void Edit()
        {
            CurrentState = State.Edit;
        }

        private void Save()
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

            CurrentState = State.None;
        }

        private void Deny()
        {
            CurrentState = State.None;
        }
    }
}
