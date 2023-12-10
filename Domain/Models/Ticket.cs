namespace Domain.Models
{
    public class Ticket : EntityBase
    {
        public Run Run { get; set; }
        public Passport PassengerPassport { get; set; }
        public TicketType Type { get; set; }
        public int? Price { get; set; }
        public DateOnly? BookDate { get; set; }
        public string? Cashier { get; set; }
    }
}
