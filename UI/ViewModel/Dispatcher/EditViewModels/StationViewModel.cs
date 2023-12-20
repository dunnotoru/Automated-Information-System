using Domain.Models;
using Domain.RepositoryInterfaces;

namespace UI.ViewModel.Dispatcher.EditViewModels
{
    internal class StationViewModel : ViewModelBase
    {
        private readonly IStationRepository _stationRepository;
        private int _id;
        private string _name;
        private string _address;

        public int Id
        {
            get { return _id; }
            private set { _id = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }

        public StationViewModel(Station station, IStationRepository stationRepository)
        {
            Id = station.Id;
            Name = station.Name ?? "";
            Address = station.Address ?? "";
            _stationRepository = stationRepository;
        }

        public Station GetStation()
        {
            return _stationRepository.GetById(Id);
        }
    }
}