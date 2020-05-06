using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETWebApplication.Helpers
{
    public class LiteDbDocumentService : ILiteDbDocumentService
    {
        private LiteDB.LiteDatabase _db;
        private string _con;

        public LiteDbDocumentService(ILiteDbContext liteDbContext)
        {
            _db = liteDbContext.Database;
            _con = liteDbContext.SqliteConnection;
        }

        public IEnumerable<Models.Document> GetAllDocuments()
        {
            //return _db.GetCollection<Models.Document>("Documents").FindAll();

            using (Microsoft.Data.Sqlite.SqliteConnection connection = new Microsoft.Data.Sqlite.SqliteConnection(_con))
            {
                connection.Open();
                using (Microsoft.Data.Sqlite.SqliteCommand command = new Microsoft.Data.Sqlite.SqliteCommand("SELECT * FROM Documents", connection))
                {
                    using (Microsoft.Data.Sqlite.SqliteDataReader reader = command.ExecuteReader())
                    {
                        List<Models.Document> documents = new List<Models.Document>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                documents.Add(new Models.Document(int.Parse(reader["Id"].ToString()), reader["Filename"].ToString(), reader["Data"].ToString()));
                            }
                        }
                        return documents;
                    }
                }
            }
        }

        public Models.Document GetDocument(int id)
        {
            //return _db.GetCollection<Models.Document>("Documents").Find(d => d.Id == id).FirstOrDefault();

            using (Microsoft.Data.Sqlite.SqliteConnection connection = new Microsoft.Data.Sqlite.SqliteConnection(_con))
            {
                connection.Open();
                using (Microsoft.Data.Sqlite.SqliteCommand command = new Microsoft.Data.Sqlite.SqliteCommand("SELECT * FROM Documents WHERE [Id]=id", connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    using (Microsoft.Data.Sqlite.SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return new Models.Document(int.Parse(reader["Id"].ToString()), reader["Filename"].ToString(), reader["Data"].ToString());
                            }
                        }
                        return new Models.Document();
                    }
                }
            }
        }

        public int InsertDocument(Models.Document document)
        {
            //return _db.GetCollection<Models.Document>("Api").Insert(document);

            using (Microsoft.Data.Sqlite.SqliteConnection connection = new Microsoft.Data.Sqlite.SqliteConnection(_con))
            {
                connection.Open();
                using (Microsoft.Data.Sqlite.SqliteCommand command = new Microsoft.Data.Sqlite.SqliteCommand("INSERT INTO Documents ([Id], [Filename], [Data]) VALUES (@id, @filename, @data)", connection))
                {
                    command.Parameters.AddWithValue("@id", document.Id);
                    command.Parameters.AddWithValue("@filename", document.Filename);
                    command.Parameters.AddWithValue("@data", document.Data);

                    return command.ExecuteNonQuery();
                }
            }
        }

        public bool UpdateDocument(Models.Document document)
        {
            //return _db.GetCollection<Models.Document>("Api").Update(document);

            using (Microsoft.Data.Sqlite.SqliteConnection connection = new Microsoft.Data.Sqlite.SqliteConnection(_con))
            {
                connection.Open();
                using (Microsoft.Data.Sqlite.SqliteCommand command = new Microsoft.Data.Sqlite.SqliteCommand("UPDATE Documents SET [Filename]=@filename, [Data]=@data WHERE [Id]=@id", connection))
                {                    
                    command.Parameters.AddWithValue("@filename", document.Filename);
                    command.Parameters.AddWithValue("@data", document.Data);
                    command.Parameters.AddWithValue("@id", document.Id);

                    return (command.ExecuteNonQuery() == 1 ? true : false);
                }
            }
        }

        public bool DeleteDocument(int id)
        {
            //return _db.GetCollection<Models.Document>("Api").Delete(id);

            using (Microsoft.Data.Sqlite.SqliteConnection connection = new Microsoft.Data.Sqlite.SqliteConnection(_con))
            {
                connection.Open();
                using (Microsoft.Data.Sqlite.SqliteCommand command = new Microsoft.Data.Sqlite.SqliteCommand("DELETE FROM Documents WHERE [Id]=id", connection))
                {
                    command.Parameters.AddWithValue("id", id);

                    return (command.ExecuteNonQuery() == 1 ? true : false);
                }
            }
        }
    }
}
