namespace Domain.Models
{
    public class TicketType 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PriceModifierInPercent { get; set; }
    }
}