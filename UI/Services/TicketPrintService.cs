using Domain.Models;
using System.IO;
using System;
using Domain.Services.Abstractions;

namespace UI.Services;

public class TicketPrintService : ITicketPrintService
{
    private readonly string _path;
    private readonly IDocumentFormatter<Ticket> _documentFormatter;

    public TicketPrintService(string path, IDocumentFormatter<Ticket> documentFormatter)
    {
        _path = path;
        _documentFormatter = documentFormatter;
    }

    public void Print(Ticket ticket)
    {

        string data = _documentFormatter.GetFormattedData(ticket);
        string name = $"Билет {ticket.IdentityDocument.GetFullName()} " + DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss") + ".txt";
        string fullPath = Path.Combine(_path, name);

        using (FileStream fs = File.Create(fullPath))
        {
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(data);
            sw.Dispose();
        }
    }
}