
using InformationSystem.Domain.Models;

namespace InformationSystem.Domain.Services.Abstractions;

public interface ITicketPrintService
{
    void Print(Ticket ticket);
}