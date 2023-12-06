using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Dynamic;

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
            _stationRepository.Add(station);
            _stationRepository.Save();
        }
        public void Update(Station station)
        {
            _stationRepository.Update(station);
            _stationRepository.Save();
        }
        public void Delete(Station station)
        {
            if (station == null) return;
            Station? storedStation = _stationRepository.GetById(station.Id);
            if (storedStation == null) return;
            _stationRepository.Delete(storedStation);
            _stationRepository.Save();
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

        public IEnumerable<Station> GetAll()
        {
            return _stationRepository.GetAll();
        }

    }
}
