namespace Domain.Models
{
    public class Route
    {
        public Route(int? id, string name, ICollection<Station> stations)
        {
            Id = id;
            Name = name;
            Stations = stations;
        }

        public Route()
        {
            
        }

        public int? Id { get; set; }
        public string Name { get; set; }
        public ICollection<Station> Stations { get; set; }
    }
}
