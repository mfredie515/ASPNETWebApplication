using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETWebApplication.Helpers
{
    public class TodoTaskService : ITodoTaskService
    {
        private const string SQL_GET_ALL = "SELECT * FROM main.TodoTasks;";
        private const string SQL_GET_ID = "SELECT * FROM main.TodoTasks WHERE id=@id;";
        private const string SQL_INSERT = "";
        private const string SQL_UPDATE = "";
        private const string SQL_DELETE = "";

        private IDbContext DbContext;

        public TodoTaskService(IDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        IEnumerable<Models.TodoTask> ITodoTaskService.GetAllTodoTasks()
        {
            using (MySqlConnection connection = new MySqlConnection(DbContext.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SQL_GET_ALL, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        List<Models.TodoTask> todoTasks = new List<Models.TodoTask>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                todoTasks.Add(new Models.TodoTask(int.Parse(reader["Id"].ToString()), reader["Title"].ToString(), reader["Description"].ToString(), reader["Location"].ToString(), reader["AddedDate"].ToString()));
                            }
                        }
                        return todoTasks;
                    }
                }
            }
        }

        Models.TodoTask ITodoTaskService.GetTodoTask(int id)
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
                                return new Models.TodoTask(int.Parse(reader["Id"].ToString()), reader["Title"].ToString(), reader["Description"].ToString(), reader["Location"].ToString(), reader["AddedDate"].ToString());
                            }
                        }
                        return new Models.TodoTask();
                    }
                }
            }
        }

        int ITodoTaskService.InsertTodoTask(Models.TodoTask todoTask)
        {
            using (MySqlConnection connection = new MySqlConnection(DbContext.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SQL_INSERT, connection))
                {
                    command.Parameters.AddWithValue("@id", todoTask.Id);
                    command.Parameters.AddWithValue("@title", todoTask.Title);
                    command.Parameters.AddWithValue("@description", todoTask.Description);
                    command.Parameters.AddWithValue("@location", todoTask.Location);
                    command.Parameters.AddWithValue("@addeddate", todoTask.AddedDate);

                    return command.ExecuteNonQuery();
                }
            }
        }

        bool ITodoTaskService.UpdateTodoTask(Models.TodoTask todoTask)
        {
            using (MySqlConnection connection = new MySqlConnection(DbContext.ConnectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(SQL_UPDATE, connection))
                {
                    command.Parameters.AddWithValue("@title", todoTask.Title);
                    command.Parameters.AddWithValue("@description", todoTask.Description);
                    command.Parameters.AddWithValue("@location", todoTask.Location);
                    command.Parameters.AddWithValue("@addeddate", todoTask.AddedDate);
                    command.Parameters.AddWithValue("@id", todoTask.Id);

                    return (command.ExecuteNonQuery() > 0 ? true : false);
                }
            }
        }

        bool ITodoTaskService.DeleteTodoTask(int id)
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
