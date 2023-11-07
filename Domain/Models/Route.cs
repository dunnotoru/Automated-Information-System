namespace Domain.Models
{
    public class Route
    {
        public Route(int id, string name, 
            List<Station> stations)
        {
            Id = id;
            Name = name;
            Stations = stations;
        }

        public int Id { get; }
        public string Name { get; }
        public List<Station> Stations { get; }
    }
}
