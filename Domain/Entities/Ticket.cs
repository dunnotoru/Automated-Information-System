namespace Domain.Entities
{
    public class Ticket
    {
        public Ticket(int id, Run run, 
            Passport passengerPassport, TicketType type)
        {
            Id = id;
            Run = run;
            PassengerPassport = passengerPassport;
            Type = type;
        }

        public int Id { get; }
        public Run Run { get; }
        public Passport PassengerPassport { get; }
        public TicketType Type { get; }
    }
}
