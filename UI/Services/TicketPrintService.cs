using Domain.Models;
using Domain.Services;
using System.IO;
using System;

namespace UI.Services
{
    public class TicketPrintService : ITicketPrintService
    {
        private readonly string _path;

        public TicketPrintService(string path)
        {
            _path = path;
        }

        public void Print(IDocumentFormatter document)
        {
            string data = document.GetFormattedData();
            string name = "Билет " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".txt";
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
