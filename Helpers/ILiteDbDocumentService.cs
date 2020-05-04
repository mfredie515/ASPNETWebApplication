using System.Collections.Generic;

namespace ASPNETWebApplication.Helpers
{
    public interface ILiteDbDocumentService
    {
        IEnumerable<Models.Document> GetAllDocuments();
        Models.Document GetDocument(int id);
        int InsertDocument(Models.Document document);
        bool UpdateDocument(Models.Document document);
        bool DeleteDocument(int id);
    }
}