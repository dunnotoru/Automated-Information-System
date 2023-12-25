using Domain.Models;

namespace Domain.Services
{
    public interface IReceiptPrintService
    {
        void Print(Receipt document);
    }
}
