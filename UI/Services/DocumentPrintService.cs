using Domain.Services;

namespace UI.Services
{
    public class DocumentPrintService : IDocumentPrintService
    {
        public void PrintDocument(IDocument document)
        {
            document.PrintDocument();   
        }
    }
}
