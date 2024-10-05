namespace Domain.Services.Abstractions;

public interface IDocumentFormatter<T>
{
    string GetFormattedData(T document);
}