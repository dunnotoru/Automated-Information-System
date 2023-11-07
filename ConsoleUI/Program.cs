using ConsoleUI.Controllers;
using Domain.EntityFramework.Repositories;
using Domain.RepositoryInterfaces.PassportRepository;
using Domain.RepositoryInterfaces.TicketRepository;
using Domain.Services.TicketSales;

namespace ConsoleUI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
            
        }

        static async Task MainAsync(string[] args)
        {
            IPassportRepository pr = new PassportRepository();
            ITicketRepository tr = new TicketRepository();
            TicketSalesService ticketSalesService
                = new TicketSalesService(pr, tr);

            ConsoleTicketSalesController controller
                = new ConsoleTicketSalesController(ticketSalesService);

            await controller.SellTicket(1234, 567890, "sd",
                "asd","asd",DateOnly.MaxValue);
        }
    }
}