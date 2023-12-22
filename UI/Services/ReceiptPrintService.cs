using Domain.Models;
using Domain.Services;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Windows.Media;

namespace UI.Services
{
    internal class ReceiptPrintService : IReceiptPrintService
    {
        private readonly string _path;

        public ReceiptPrintService(string path)
        {
            _path = path;
        }

        public void Print(IDocumentFormatter document)
        {
            string data = document.GetFormattedData();
            string name = "Чек " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".txt";
            string fullPath = Path.Combine(_path, name);
            
            using (FileStream fs = File.Create(fullPath))
            {
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(data);
                sw.Dispose();
            }
        }
    }
}
