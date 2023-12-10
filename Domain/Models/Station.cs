namespace Domain.Models
{
    public class Station : EntityBase
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public Station()
        {
            Routes = new HashSet<Route>();
        }

        public static Station GetClone(Station station)
        {
            ArgumentNullException.ThrowIfNull(station);

            return new Station()
            {
                Id = station.Id,
                Name = station.Name,
                Address = station.Address,
            };
        }
    }
}