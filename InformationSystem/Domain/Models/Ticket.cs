using System;

namespace InformationSystem.Domain.Models;

public class Ticket : EntityBase
{
    public int Price { get; set; }
    public DateTime BookDate { get; set; }
    public string Cashier { get; set; }
        

    public int RunId { get; set; }
    public int IdentityDocumentId { get; set; }
    public int TicketTypeId { get; set; }
    public Run Run { get; set; }
    public IdentityDocument IdentityDocument { get; set; }
    public TicketType TicketType { get; set; }

}