using Domain.Models;

namespace Domain.Services.Abstractions;

public interface IReceiptPrintService
{
    void Print(Receipt document);
}