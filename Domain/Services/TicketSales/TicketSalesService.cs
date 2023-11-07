using Domain.RepositoryInterfaces.PassportRepository;
using Domain.RepositoryInterfaces.TicketRepository;

namespace Domain.Services.TicketSales
{
    public class TicketSalesService
    {
        private IPassportRepository _passportRepository;
        private ITicketRepository _ticketRepository;

        public TicketSalesService(IPassportRepository passportRepository, 
            ITicketRepository ticketRepository)
        {
            _passportRepository = passportRepository;
            _ticketRepository = ticketRepository;
        }

        public Task<bool> SellTicket(int number, int series, 
            string name, string surname, string patro, DateOnly birthday)
        {
            throw new NotFiniteNumberException();
        }
    }
}
