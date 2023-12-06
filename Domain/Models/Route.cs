namespace Domain.Models
{
    public class Route
    {
        public Route(int id, string name, ICollection<Station> stations)
        {
            Id = id;
            Name = name;
            Stations = stations;
        }

        public Route()
        {
            Stations = new List<Station>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Station> Stations { get; set; }
    }
}
