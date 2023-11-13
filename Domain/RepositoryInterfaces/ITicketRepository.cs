using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface ITicketRepository 
    {
        bool AddAsync(Ticket ticket);
        bool UpdateAsync(Ticket ticket);
        bool DeleteAsync(int id);
        Ticket GetAsync(int id);
        Ticket GetAsync(Passport passport, Run run);
        IEnumerable<Ticket> GetAsync(Run run);
        IEnumerable<Ticket> GetAsync(Passport passport);
        IEnumerable<Ticket> GetAllAsync();
    }
}
