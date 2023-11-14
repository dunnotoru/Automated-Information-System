namespace Domain.Models
{
    public class Ticket 
    {
        public int Id { get; set; }
        public Run Run { get; set; }
        public Passport PassengerPassport { get; set; }
        public TicketType Type { get; set; }
        public int Price { get; set; }
        public DateOnly BookDate { get; set; }
        public string Casshier { get; set; }
    }
}
