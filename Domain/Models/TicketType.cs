﻿using Domain.Exceptions;

namespace Domain.Models
{
    public class TicketType : DomainObject
    {
        public TicketType(int id, string name, int priceModifierInPercent)
        {
            Id = id;
            Name = name;
            PriceModifierInPercent = priceModifierInPercent;
        }

        public string Name { get; }
        public int PriceModifierInPercent
        {
            get => _priceModifierInPercent;
            set
            {
                if (value < 0 || value > 100)
                    throw new OutOfPercentRangeException(nameof(PriceModifierInPercent));
                _priceModifierInPercent = value;
            }
        }

        private int _priceModifierInPercent;
    }
}