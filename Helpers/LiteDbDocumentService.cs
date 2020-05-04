using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETWebApplication.Helpers
{
    public class LiteDbDocumentService : ILiteDbDocumentService
    {
        private LiteDB.LiteDatabase _db;

        public LiteDbDocumentService(ILiteDbContext liteDbContext)
        {
            _db = liteDbContext.Database;
        }

        public IEnumerable<Models.Document> GetAllDocuments()
        {
            return _db.GetCollection<Models.Document>("Documents").FindAll();
        }

        public Models.Document GetDocument(int id)
        {
            return _db.GetCollection<Models.Document>("Documents").Find(d => d.Id == id).FirstOrDefault();
        }

        public int InsertDocument(Models.Document document)
        {
            return _db.GetCollection<Models.Document>("Api").Insert(document);
        }

        public bool UpdateDocument(Models.Document document)
        {
            return _db.GetCollection<Models.Document>("Api").Update(document);
        }

        public bool DeleteDocument(int id)
        {
            return _db.GetCollection<Models.Document>("Api").Delete(id);
        }
    }
}
