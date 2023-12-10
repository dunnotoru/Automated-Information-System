namespace Domain.Models
{
    public class TicketType : EntityBase
    {
        public string? Name { get; set; }
        public int? PriceModifierInPercent { get; set; }
    }
}