namespace Domain.Models
{
    public class Station 
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
        public Station()
        {
            Routes = new HashSet<Route>();
        }
    }
}