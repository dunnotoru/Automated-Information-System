using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.Services
{
    public class StationService
    {
        private readonly IStationRepository _stationRepository;

        public StationService(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public void Add(Station station)
        {
            ArgumentNullException.ThrowIfNull(station);

            _stationRepository.Add(station);
        }
        public void Update(Station station)
        {
            ArgumentNullException.ThrowIfNull(station);

            _stationRepository.Update(station);
        }
        public void Delete(Station station)
        {
            ArgumentNullException.ThrowIfNull(station);
            Station? storedStation = _stationRepository.GetById(station.Id);
            
            if (storedStation == null) return;
            _stationRepository.Remove(storedStation);
        }

        public Station? GetById(int id)
        {
            return _stationRepository.GetById(id);
        }

        public IEnumerable<Station> GetByName(string name)
        {
            return _stationRepository.GetByName(name);
        }

        public IEnumerable<Station> GetByAddress(string address)
        {
            return _stationRepository.GetByAddress(address);
        }

        public List<Station> GetAll()
        {
            return _stationRepository.GetAll().ToList();
        }
    }
}
