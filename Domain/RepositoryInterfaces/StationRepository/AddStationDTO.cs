namespace Domain.RepositoryInterfaces.StationRepository
{
    public class AddStationDTO
    {
        public AddStationDTO(string name, string address)
        {
            Name = name;
            Address = address;
        }

        public string Name { get; }
        public string Address { get; }
    }
}
