using Domain.Models;
using Domain.Services;
using System.IO;

namespace UI.Services
{
    public class ReceiptPrint : IDocumentPrint
    {
        private readonly Receipt _receipt;
        public ReceiptPrint(Receipt receipt)
        {
            _receipt = receipt;
        }
        public void PrintDocument()
        {

            File.Create("receipt.txt");
            using (StreamWriter sw = new StreamWriter("receipt.txt"))
            {
                sw.WriteLine(_receipt.Number);
            }
        }
    }
}
