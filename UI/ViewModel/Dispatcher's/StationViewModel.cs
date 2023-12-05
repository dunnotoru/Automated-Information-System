using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public enum State
    {
        None = 0,
        Add,
        Edit
    }

    public class StationViewModel : ViewModelBase
    {
        private readonly IStationRepository _stationRepository;
        private Station _selectedItem;
        private State _currentState;

        public ObservableCollection<Station> Items { get; set; }
        public Station SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; NotifyPropertyChanged(nameof(SelectedItem)); }
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

        public ICommand AddStationCommand 
            => new RelayCommand(AddStation, () => CurrentState == State.None);
        public ICommand DeleteStationCommand
            => new RelayCommand(DeleteStation, () => CurrentState == State.None);
        public ICommand EditStationCommand
            => new RelayCommand(EditStation, () => CurrentState == State.None);
        public ICommand SaveChangesCommand
            => new RelayCommand(SaveChanges, () => CurrentState == State.Add || CurrentState == State.Edit);

        public StationViewModel(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
            CurrentState = State.None;
            Items = new ObservableCollection<Station>(_stationRepository.GetAll());
        }

        private void AddStation()
        {
            SelectedItem = new Station();
            CurrentState = State.Add;
        }

        private void DeleteStation()
        {
            if (SelectedItem == null) return;
            Station? storedStation = _stationRepository.GetById(SelectedItem.Id);
            if (storedStation == null) return;
            _stationRepository.Delete(storedStation);
            if (storedStation == null) return;
            Items.Remove(storedStation);
        }

        private void EditStation()
        {
            CurrentState = State.Edit;
        }

        private void SaveChanges()
        {
            if(CurrentState == State.Add)
            {
                _stationRepository.Add(SelectedItem);
                Items.Add(SelectedItem);
            }
            else if(CurrentState == State.Edit)
            {
                _stationRepository.Update(SelectedItem);
            }
            
            _stationRepository.Save();
            CurrentState = State.None;
        }
    }
}
