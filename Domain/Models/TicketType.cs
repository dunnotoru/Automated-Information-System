namespace Domain.Models
{
    public class TicketType 
    {
        public TicketType(int id, string name, int priceModifierInPercent)
        {
            Id = id;
            Name = name;
            PriceModifierInPercent = priceModifierInPercent;
        }

        public int Id { get; }
        public string Name { get; }
        public int PriceModifierInPercent { get; }
    }
}