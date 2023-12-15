using Domain.Models;
using Domain.RepositoryInterfaces;
using System;
using System.Collections.Specialized;
using System.Windows.Input;
using UI.Command;

namespace UI.ViewModel.Dispatcher.EntityViewModel
{
    public class StationEditViewModel : ViewModelBase
    {
        private readonly IStationRepository _stationRepository;
        private string _name;
        private string _address;

        public Action RemoveEvent;
        public Action<string> ErrorEvent;

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }
        
        public StationEditViewModel(Station station, IStationRepository stationRepository) : base()
        {
            ArgumentNullException.ThrowIfNull(station);
            ArgumentNullException.ThrowIfNull(stationRepository);

            Id = station.Id;
            Name = station.Name ?? "";
            Address = station.Address ?? "";
            _stationRepository = stationRepository;
        }

        public StationEditViewModel(IStationRepository stationRepository) : base()
        {
            ArgumentNullException.ThrowIfNull(stationRepository);

            Id = 0;
            Name = "Unknown";
            Address = "Unknown";
            
            _stationRepository = stationRepository;
        }

        private StationEditViewModel()
        {
            AddCommand = new RelayCommand(Add);
            RemoveCommand = new RelayCommand(Remove);
        }

        public int Id { get; set; }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChangedByCallerName(); }
        }
        public string Address
        {
            get => _address;
            set { _address = value; OnPropertyChangedByCallerName(); }
        }

        public void Add()
        {
            try
            {
                if (Id == 0)
                {
                    Station createdStation = new Station()
                    {
                        Name = Name,
                        Address = Address,
                    };

                    _stationRepository.Add(createdStation);
                }
                else
                {
                    Station updatedStation = new Station()
                    {
                        Id = Id,
                        Name = Name,
                        Address = Address,
                    };
                    _stationRepository.Update(updatedStation.Id, updatedStation);
                }
            }
            catch(Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }
        }

        public void Remove()
        {
            try
            {
                if(Id != 0)
                {
                    _stationRepository.Remove(Id);
                }
            }
            catch (Exception e)
            {
                ErrorEvent?.Invoke(e.Message);
            }

            RemoveEvent?.Invoke();
        }
    }
}
