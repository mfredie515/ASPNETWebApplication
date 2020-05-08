using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ASPNETWebApplication.Helpers
{
    public class DocumentService : IDocumentService
    {
        private const string SQL_GET_ALL = "SELECT * FROM main.Documents;";
        private const string SQL_GET_ID = "SELECT * FROM main.Documents WHERE id=@id;";
        private const string SQL_INSERT = "";
        private const string SQL_UPDATE = "";
        private const string SQL_DELETE = "";

        private IDbContext DbContext;
        
        public DocumentService(IDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public IEnumerable<Models.Document> GetAllDocuments()
        {
            using (MySqlConnection connection = new MySqlConnection(DbContext.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SQL_GET_ALL, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<Models.Document> documents = new List<Models.Document>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                documents.Add(new Models.Document(int.Parse(reader["Id"].ToString()), reader["Filename"].ToString(), (byte[])reader["Data"]));
                            }
                        }
                        return documents;
                    }
                }
            }
        }

        public Models.Document GetDocument(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(DbContext.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SQL_GET_ID, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return new Models.Document(int.Parse(reader["Id"].ToString()), reader["Filename"].ToString(), (byte[])reader["Data"]);
                            }
                        }
                        return new Models.Document();
                    }
                }
            }
        }

        public int InsertDocument(Models.Document document)
        {
            using (MySqlConnection connection = new MySqlConnection(DbContext.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SQL_INSERT, connection))
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
            using (MySqlConnection connection = new MySqlConnection(DbContext.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SQL_UPDATE, connection))
                {
                    command.Parameters.AddWithValue("@filename", document.Filename);
                    command.Parameters.AddWithValue("@data", document.Data);
                    command.Parameters.AddWithValue("@id", document.Id);

                    return (command.ExecuteNonQuery() > 0 ? true : false);
                }
            }
        }

        public bool DeleteDocument(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(DbContext.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SQL_DELETE, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    return (command.ExecuteNonQuery() > 0 ? true : false);
                }
            }
        }
    }
}
