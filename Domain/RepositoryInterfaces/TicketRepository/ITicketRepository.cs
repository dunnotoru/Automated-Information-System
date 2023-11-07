using Domain.Models;

namespace Domain.RepositoryInterfaces.TicketRepository
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
        Task<Ticket> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}