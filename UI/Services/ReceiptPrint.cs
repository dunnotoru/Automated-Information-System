using Accessibility;
using Domain.Models;
using Domain.Services;
using System.IO;
using System.Text;

namespace UI.Services
{
    public class ReceiptPrint : IDocument
    {
        private readonly Receipt _receipt;
        public ReceiptPrint(Receipt receipt)
        {
            _receipt = receipt;
        }
        public void PrintDocument()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(_receipt.Number);
            sb.AppendLine(_receipt.CompanyName);
            sb.AppendLine(_receipt.Address);
            sb.AppendLine(_receipt.CashierName);
            sb.AppendLine(_receipt.OperationDateTime.ToString());
            foreach (ReceiptLine line in _receipt.ReceiptLines)
            {
                sb.AppendLine($"{line.Header}....{line.Count}x{line.Price}....{line.FullPrice}");
            }
            sb.AppendLine("ИТОГО:");
            sb.AppendLine(_receipt.FullPrice.ToString());

            using (StreamWriter sw = new StreamWriter("receipt.txt"))
            {
                sw.Write(sb.ToString());
            }
        }
    }
}
