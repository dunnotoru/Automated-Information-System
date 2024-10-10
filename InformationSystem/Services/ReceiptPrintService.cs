using System;
using System.IO;
using InformationSystem.Domain;
using InformationSystem.Domain.Models;
using InformationSystem.Services.Abstractions;

namespace InformationSystem.Services;

internal class ReceiptPrintService : IReceiptPrintService
{
    private readonly string _path;
    private readonly IDocumentFormatter<Receipt> _documentFormatter;

    public ReceiptPrintService(string path, IDocumentFormatter<Receipt> documentFormatter)
    {
        _path = path;
        _documentFormatter = documentFormatter;
    }

    public void Print(Receipt document)
    {
        string data = _documentFormatter.GetFormattedData(document);
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