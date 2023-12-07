using Domain.Models;
using Domain.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public class StationManagerViewModel : ViewModelBase
    {
        private readonly StationService _stationService;
        private Station _selectedStation;
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
                NotifyPropertyChanged(nameof(CanChangeSelect)); 
                NotifyPropertyChanged(nameof(IsReadOnly)); 
            }
        }
        public bool CanChangeSelect => CurrentState == State.None;
        public bool IsReadOnly => CurrentState == State.None;

        public ICommand AddCommand {  get; }
        public ICommand DeleteCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand DenyCommand { get; }

        public StationManagerViewModel(StationService stationService)
        {
            _stationService = stationService;
            CurrentState = State.None;
            Stations = new ObservableCollection<Station>(_stationService.GetAll());

            AddCommand = new RelayCommand(Add, () => CurrentState == State.None);
            DeleteCommand = new RelayCommand(Delete, () => CurrentState == State.None);
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
            if (SelectedStation == null) return;
            if (!Stations.Contains(SelectedStation)) return;
            _stationService.Delete(SelectedStation);
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
                _stationService.Add(SelectedStation);
                Stations.Add(SelectedStation);
            }
            else if(CurrentState == State.Edit)
            {
                _stationService.Update(SelectedStation);
            }

            CurrentState = State.None;
        }

        private void Deny()
        {
            CurrentState = State.None;
        }
    }
}
