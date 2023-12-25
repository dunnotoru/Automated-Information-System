using Domain.Models;
using Domain.Services;

namespace UI.Services
{
    public class ReceiptFormatter : IDocumentFormatter<Receipt>
    {
        public string GetFormattedData(Receipt docuement)
        {
            return "ЧЕК";
        }
    }
}
