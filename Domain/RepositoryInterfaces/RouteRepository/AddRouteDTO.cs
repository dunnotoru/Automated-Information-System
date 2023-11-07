using Domain.Models;

namespace Domain.RepositoryInterfaces.RouteRepository
{
    public class AddRouteDTO
    {
        public AddRouteDTO(string name, List<Station> stations)
        {
            Name = name;
            Stations = stations;
        }

        public string Name { get; }
        public List<Station> Stations { get; }
    }
}
