namespace Domain.Models
{
    public class Station : EntityBase
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Route> Routes { get; set; } = new List<Route>();
    }
}