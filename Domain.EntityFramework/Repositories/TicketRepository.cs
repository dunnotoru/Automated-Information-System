using Domain.Entities;
using Domain.RepositoryInterfaces.TicketRepository;
using Domain.RepositoryInterfaces.TicketRepository.DTOs;

namespace Domain.EntityFramework.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        public Task<bool> AddAsync(TicketDTO ticketDTO)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
