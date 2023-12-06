using Domain.Models;
using Domain.RepositoryInterfaces;
using Domain.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel
{
    public class StationManagerViewModel : ViewModelBase
    {
        private readonly StationService _stationService;
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

        public ICommand AddCommand 
            => new RelayCommand(Add, () => CurrentState == State.None);
        public ICommand DeleteCommand
            => new RelayCommand(Delete, () => CurrentState == State.None);
        public ICommand EditCommand
            => new RelayCommand(Edit, () => CurrentState == State.None && SelectedItem != null);
        public ICommand SaveCommand
            => new RelayCommand(Save, () => CurrentState == State.Add || CurrentState == State.Edit);
        public ICommand DenyCommand
            => new RelayCommand(Deny, () => CurrentState != State.None);

        public StationManagerViewModel(StationService stationService)
        {
            _stationService = stationService;
            CurrentState = State.None;
            Items = new ObservableCollection<Station>(_stationService.GetAll());
        }

        private void Add()
        {
            CurrentState = State.Add;
            SelectedItem = new Station();
        }

        private void Delete()
        {
            _stationService.Delete(SelectedItem);
            if (!Items.Contains(SelectedItem)) return;
            Items.Remove(SelectedItem);
        }

        private void Edit()
        {
            CurrentState = State.Edit;
        }

        private void Save()
        {
            if(CurrentState == State.Add)
            {
                _stationService.Add(SelectedItem);
                Items.Add(SelectedItem);
            }
            else if(CurrentState == State.Edit)
            {
                _stationService.Update(SelectedItem);
            }

            CurrentState = State.None;
        }

        private void Deny()
        {
            CurrentState = State.None;
        }
    }
}
