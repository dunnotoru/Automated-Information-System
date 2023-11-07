using Domain.Services.TicketSales;

namespace ConsoleUI.Controllers
{
    public class ConsoleTicketSalesController
    {
        private TicketSalesService _ticketSalesService;

        public ConsoleTicketSalesController(TicketSalesService ticketSalesService)
        {
            _ticketSalesService = ticketSalesService;
        }

        public async Task<bool> SellTicket(int number, int series,
            string name, string surname, string patro, DateOnly birthday)
        {
            return await _ticketSalesService.SellTicket(number, series,
                name,surname, patro,birthday);
        }
    }
}
