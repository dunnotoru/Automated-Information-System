using Domain.Models;
using Domain.Services;

namespace UI.Services
{
    public class ReceiptFormatter : IDocumentFormatter
    {
        private readonly Receipt _receipt;

        public ReceiptFormatter(Receipt receipt)
        {
            _receipt = receipt;
        }

        public string GetFormattedData()
        {
            return "ЧЕК";
        }
    }
}
