using Domain.Models;
using Domain.RepositoryInterfaces;

namespace Domain.UseCases.CasshierUseCases
{
    public class GetStationsUseCase
    {
        private readonly IStationRepository _stationRepository;

        public GetStationsUseCase(IStationRepository stationRepository)
        {
            _stationRepository = stationRepository;
        }

        public IEnumerable<Station> GetStations()
        {
            return _stationRepository.GetAll();
        }
    }
}
