namespace Domain.Models
{
    public class Route : EntityBase
    {
        public string? Name { get; set; }
        public ICollection<Station> Stations { get; set; } = new List<Station>();
    }
}
