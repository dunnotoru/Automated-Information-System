using Domain.Models;

namespace Domain.RepositoryInterfaces.TicketRepository
{
    public interface ITicketRepository
    {
        Task<bool> AddAsync(AddTicketDTO ticketDTO);
        Task<Ticket> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}