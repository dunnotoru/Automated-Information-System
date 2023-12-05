using Domain.Models;
using Domain.UseCases.CashierUseCases;

namespace Domain.Tests.TicketSaleTest
{
    internal class TicketPriceCalculatorStub : ITicketPriceCalculator
    {
        public int CalcPrice(Route route, Station from, Station to, TicketType type)
        {
            return 999 * type.PriceModifierInPercent;
        }
    }
}
