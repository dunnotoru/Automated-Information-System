namespace Domain.Entities
{
    public class Ticket
    {
        public Ticket(int id, Run route, 
            Passport passengerPassport, TicketType type)
        {
            Id = id;
            Route = route;
            PassengerPassport = passengerPassport;
            Type = type;
        }

        public int Id { get; }
        public Run Route { get; }
        public Passport PassengerPassport { get; }
        public TicketType Type { get; }
    }
}
