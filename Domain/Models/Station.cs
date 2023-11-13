namespace Domain.Models
{
    public class Station 
    {
        public Station(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public int Id { get; }
        public string Name { get; }
        public string Address { get; }
    }
}
