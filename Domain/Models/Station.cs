namespace Domain.Models
{
    public class Station : EntityBase
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public ICollection<Route> Routes { get; set; } = new List<Route>();
    }
}