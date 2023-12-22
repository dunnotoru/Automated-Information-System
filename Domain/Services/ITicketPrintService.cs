using Domain.Models;

namespace Domain.Services
{
    public interface ITicketPrintService
    {
        void Print(IDocumentFormatter document);
    }
}
