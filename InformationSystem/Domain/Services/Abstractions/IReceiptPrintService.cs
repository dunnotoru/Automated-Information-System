using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.Services.Abstractions;

public interface IReceiptPrintService
{
    void Print(Receipt document);
}