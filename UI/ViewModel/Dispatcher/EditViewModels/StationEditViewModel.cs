using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Windows.Input;
using UI.Command;
using UI.Services;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    internal class StationEditViewModel : ViewModelBase
    {
        private readonly IStationRepository _stationRepository;

        private string _name;
        private string _address;

        public event EventHandler Remove;
        public event EventHandler Save;
        public event EventHandler<Exception> Error;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public StationEditViewModel(Station station, IStationRepository stationRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(station);
            ArgumentNullException.ThrowIfNull(stationRepository);
            _stationRepository = stationRepository;

            Id = station.Id;
            Name = station.Name ?? "";
            Address = station.Address ?? "";
        }

        public StationEditViewModel(IStationRepository stationRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(stationRepository);
            _stationRepository = stationRepository;

            Id = 0;
            Name = "";
            Address = "";
        }

        private StationEditViewModel()
        {
            SaveCommand = new RelayCommand(ExecuteSave);
            RemoveCommand = new RelayCommand(ExecuteRemove);
        }

        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChanged(); }
        }

        public bool CanSave()
        {
            return !string.IsNullOrWhiteSpace(Name) 
                && !string.IsNullOrWhiteSpace(Address);
        }

        public void ExecuteSave()
        {
            Station createdStation = new Station()
            {
                Name = Name,
                Address = Address,
            };

            try
            {
                if (Id == 0)
                {
                    Id = _stationRepository.Create(createdStation);
                }
                else
                {
                    _stationRepository.Update(Id, createdStation);
                }
                Save?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Error?.Invoke(this, e);
            }
        }

        public void ExecuteRemove()
        {
            if (Id == 0) return;

            try
            {
                _stationRepository.Remove(Id);
                Remove?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception e)
            {
                Error?.Invoke(this, e);
            }
        }
    }
}
