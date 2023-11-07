using Domain.Entities;
using Domain.RepositoryInterfaces.TicketRepository.DTOs;

namespace Domain.RepositoryInterfaces.TicketRepository
{
    public interface ITicketRepository
    {
        Task<bool> AddAsync(TicketDTO ticketDTO);
        Task<Ticket> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}