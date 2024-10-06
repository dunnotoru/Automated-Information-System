namespace InformationSystem.Services.Abstractions;

public interface IReceiptPrintService
{
    void Print(Receipt document);
}