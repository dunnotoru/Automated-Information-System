namespace Domain.Models
{
    public class Ticket : DomainObject
    {
        public Ticket(int id, Run run, 
            Passport passengerPassport, TicketType type)
        {
            Id = id;
            Run = run;
            PassengerPassport = passengerPassport;
            Type = type;
        }

        public Run Run { get; }
        public Passport PassengerPassport { get; }
        public TicketType Type { get; }
    }
}
