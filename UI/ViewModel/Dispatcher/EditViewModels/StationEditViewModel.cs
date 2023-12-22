using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    public class StationEditViewModel : ViewModelBase
    {
        private readonly IStationRepository _stationRepository;
        private string _name;
        private string _address;

        public Action<StationEditViewModel> RemoveEvent;
        public Action<string> ErrorEvent;

        public ICommand SaveCommand { get; }
        public ICommand RemoveCommand { get; }

        public StationEditViewModel(Station station, IStationRepository stationRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(station);
            ArgumentNullException.ThrowIfNull(stationRepository);

            Id = station.Id;
            Name = station.Name ?? "";
            Address = station.Address ?? "";

            _stationRepository = stationRepository;
        }

        public StationEditViewModel(IStationRepository stationRepository) : this()
        {
            ArgumentNullException.ThrowIfNull(stationRepository);

            Id = 0;
            Name = "";
            Address = "";
            _stationRepository = stationRepository;
        }

        private StationEditViewModel()
        {
            SaveCommand = new RelayCommand(Save);
            RemoveCommand = new RelayCommand(Remove);
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

        public void Save()
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
                    _stationRepository.Create(createdStation);
                }
                else
                {
                    _stationRepository.Update(Id, createdStation);
                }
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }

        public void Remove()
        {
            if (Id == 0) return;
            try
            {
                _stationRepository.Remove(Id);
                RemoveEvent?.Invoke(this);
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }
    }
}
