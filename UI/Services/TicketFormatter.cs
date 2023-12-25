using Domain.Models;
using Domain.Services;

namespace UI.Services
{
    public class TicketFormatter : IDocumentFormatter<Ticket>
    {
        public string GetFormattedData(Ticket ticket)
        {
            return "БИЛЕТ";
        }
    }
}
