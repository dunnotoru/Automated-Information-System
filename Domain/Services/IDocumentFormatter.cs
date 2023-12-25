namespace Domain.Services
{
    public interface IDocumentFormatter<T>
    {
        string GetFormattedData(T document);
    }
}