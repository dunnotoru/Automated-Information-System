namespace Domain.Core
{
    public class Ticket
    {
        public int Id { get; }
        public BusRoute Route { get; }
        public Passport PassengerPassport { get; }
        public TicketType Type { get; }
    }
}
