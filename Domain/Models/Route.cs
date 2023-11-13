namespace Domain.Models
{
    public class Route
    {
        public Route(string name, List<Station> stations)
        {
            Name = name;
            Stations = stations;
        }

        public string Name { get; }
        public List<Station> Stations { get; }
    }
}
