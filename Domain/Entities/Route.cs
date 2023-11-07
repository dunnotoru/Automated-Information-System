namespace Domain.Core
{
    public class Route
    {
        public Route(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; }
        public string Name { get; }
    }
}
