using Domain.Models;
using Domain.Services;

namespace UI.Services
{
    public class TicketFormatter : IDocumentFormatter
    {
        private readonly Ticket _ticket;

        public TicketFormatter(Ticket ticket)
        {
            _ticket = ticket;
        }

        public string GetFormattedData()
        {
            return "БИЛЕТ";
        }
    }
}
