using Domain.Models;
using System.Text;
using Domain.Services.Abstractions;

namespace UI.Services;

public class TicketFormatter : IDocumentFormatter<Ticket>
{
    public string GetFormattedData(Ticket ticket)
    {
        string result;
        StringBuilder sb = new StringBuilder();

        string name = ticket.IdentityDocument.Name;
        string surname = ticket.IdentityDocument.Surname;
        string patronymic = ticket.IdentityDocument.Patronymic;
        string fullName = $"{name} {surname} {patronymic}";

        sb.AppendLine($"Дата покупки: {ticket.BookDate}");
        sb.AppendLine($"Кассир: {ticket.Cashier}");
        sb.AppendLine($"Цена: {ticket.Price}");

        sb.AppendLine($"ФИО пассажира: {fullName}");
        sb.AppendLine($"Тип билета: {ticket.TicketType.Name}");
            
        sb.AppendLine($"Номер рейса: {ticket.Run.Number}");
        sb.AppendLine($"Маршрут: {ticket.Run.Route.Name}");
        sb.AppendLine($"Время отправления: {ticket.Run.DepartureDateTime}");
        sb.AppendLine($"Время прибытия: {ticket.Run.EstimatedArrivalDateTime}");
        sb.AppendLine($"Номер автобуса: {ticket.Run.Vehicle.LicensePlateNumber}");

        result = sb.ToString();

        return result;
    }
}