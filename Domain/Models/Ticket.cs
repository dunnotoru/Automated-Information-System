namespace Domain.Models
{
    public class Ticket 
    {
        public Ticket(int id, Run run, 
            Passport passengerPassport, TicketType type,
            int price, DateOnly bookDate, string casshier)
        {
            Id = id;
            Run = run;
            PassengerPassport = passengerPassport;
            Type = type;
            Price = price;
            BookDate = bookDate;
            Casshier = casshier;
        }

        public int Id { get; }
        public Run Run { get; }
        public Passport PassengerPassport { get; }
        public TicketType Type { get; }
        public int Price { get; }
        public DateOnly BookDate { get; }
        public string Casshier { get; }
    }
}
