using System.Text;
using InformationSystem.Domain.Models;
using InformationSystem.Domain.Services.Abstractions;

namespace InformationSystem.Services;

public class ReceiptFormatter : IDocumentFormatter<Receipt>
{
    public string GetFormattedData(Receipt document)
    {
        string result;

        StringBuilder sb = new StringBuilder();
            
        sb.AppendLine($"Номер чека: {document.Number}");
        sb.AppendLine($"Организация: {document.CompanyName}");
        sb.AppendLine($"Юридический адрес: {document.Address}");
        sb.AppendLine($"Время: {document.OperationDateTime}");
        sb.AppendLine($"Кассир: {document.CashierName}");
        sb.AppendLine($"Товар Цена Кол-во Стоимость");
        foreach (var item in document.ReceiptLines)
        {
            sb.AppendLine($"{item.Header}  {item.Price}  {item.Count}  {item.FullPrice}");
        }
        sb.AppendLine($"ИТОГ           {document.FullPrice}");

        result = sb.ToString();

        return result;
    }
}