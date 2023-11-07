namespace Domain.Core
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
        public int PriceModifierInPercent
        {
            get => _priceModifierInPercent;
            set
            {
                if(value < 0 || value > 100)
                    throw 
                    
            }
        }

        private int _priceModifierInPercent;
    }
}