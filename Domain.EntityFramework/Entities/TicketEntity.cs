namespace Domain.EntityFramework.Entities
{
    public class TicketEntity
    {
        public int Id { get; set; }
        public RunEntity Run { get; set; }
        public PassportEntity Passport { get; set; }
        public TicketTypeEntity Type { get; set; }
        public int Price { get; set; }
        public DateOnly BookDate { get; set; }
        public string Casshier { get; set; }
    }
}
