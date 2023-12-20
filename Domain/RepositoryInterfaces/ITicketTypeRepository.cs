using Domain.Models;

namespace Domain.RepositoryInterfaces
{
    public interface ITicketTypeRepository : IRepositoryBase<TicketType>
    {
        TicketType GetById(int id);
        IEnumerable<TicketType> GetAll();
    }
}
