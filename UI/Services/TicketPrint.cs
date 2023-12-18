using Domain.Models;
using Domain.Services;
using System.IO;

namespace UI.Services
{
    public class TicketPrint : IDocumentPrint
    {
        private readonly Ticket _ticket;
        public TicketPrint(Ticket ticket)
        {
            _ticket = ticket;
        }

        public void PrintDocument()
        {
            File.Create("text.txt");
            using (StreamWriter sw = new StreamWriter("text.txt"))
            {
                sw.WriteLine(_ticket.Id);
            }
        }
    }
}
