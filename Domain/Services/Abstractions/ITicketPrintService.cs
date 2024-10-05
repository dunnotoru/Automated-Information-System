
using Domain.Models;

namespace Domain.Services.Abstractions;

public interface ITicketPrintService
{
    void Print(Ticket ticket);
}