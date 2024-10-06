
using InformationSystem.Domain.Models;

namespace InformationSystem.Services.Abstractions;

public interface ITicketPrintService
{
    void Print(Ticket ticket);
}