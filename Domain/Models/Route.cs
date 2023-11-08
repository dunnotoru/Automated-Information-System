namespace Domain.Models
{
    public class Route : DomainObject
    {
        public Route(int id, string name, 
            List<Station> stations)
        {
            Id = id;
            Name = name;
            Stations = stations;
        }

        public string Name { get; }
        public List<Station> Stations { get; }
    }
}
