using Domain.Models;
using Domain.Services;
using System.IO;

namespace UI.Services
{
    public class TicketPrint : IDocument
    {
        private readonly Ticket _ticket;
        public TicketPrint(Ticket ticket)
        {
            _ticket = ticket;
        }

        public void PrintDocument()
        {
            using (StreamWriter sw = new StreamWriter("text.txt"))
            {
                sw.WriteLine(_ticket.Cashier);
            }
        }
    }
}
