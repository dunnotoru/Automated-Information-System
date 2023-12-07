using Domain.Models;
using Domain.RepositoryInterfaces;
using System.Diagnostics;

namespace Domain.Tests.TicketSaleTest
{
    internal class TicketRepositoryStub : ITicketRepository
    {
        private HashSet<Ticket> _tickets;

        public TicketRepositoryStub(HashSet<Ticket> tickets)
        {
            _tickets = tickets;
        }

        public void Add(Ticket entity)
        {
            _tickets.Add(entity);
        }

        public void Remove(Ticket entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            Debug.WriteLine("Passport repository saved");
        }

        public void Update(Ticket entity)
        {
            throw new NotImplementedException();
        }
    }
}
