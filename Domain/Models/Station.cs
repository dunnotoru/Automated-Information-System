namespace Domain.Models
{
    public class Station : DomainObject
    {
        public Station(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public string Name { get; }
        public string Address { get; }
    }
}
