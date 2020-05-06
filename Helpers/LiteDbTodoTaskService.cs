﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ASPNETWebApplication.Helpers
{
    public class LiteDbTodoTaskService : ILiteDbTodoTaskService
    {
        private LiteDB.LiteDatabase _db;
        private string _con;

        public LiteDbTodoTaskService(ILiteDbContext liteDbContext)
        {
            _db = liteDbContext.Database;
            _con = liteDbContext.SqliteConnection;
        }

        bool ILiteDbTodoTaskService.DeleteTodoTask(int id)
        {
            return _db.GetCollection<Models.TodoTask>("Api").Delete(id);
        }

        IEnumerable<Models.TodoTask> ILiteDbTodoTaskService.GetAllTodoTasks()
        {
            return _db.GetCollection<Models.TodoTask>("TodoTasks").FindAll();

            //using (SQLiteConnection connection = new SQLiteConnection(_con))
            //{
            //    connection.Open();
            //    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM TodoTasks", connection))
            //    {
            //        using (SQLiteDataReader reader = command.ExecuteReader())
            //        {
            //            List<Models.TodoTask> todoTasks = new List<Models.TodoTask>();
            //            if (reader.HasRows)
            //            {
            //                while (reader.Read())
            //                {
            //                    todoTasks.Add(new Models.TodoTask(int.Parse(reader["Id"].ToString()), reader["Title"].ToString(), reader["Description"].ToString(), reader["Location"].ToString(), reader["AddedDate"].ToString()));
            //                }
            //            }
            //            return todoTasks;
            //        }
            //    }
            //}
        }

        Models.TodoTask ILiteDbTodoTaskService.GetTodoTask(int id)
        {
            return _db.GetCollection<Models.TodoTask>("TodoTasks").Find(t => t.Id == id).FirstOrDefault();

            //using (SQLiteConnection connection = new SQLiteConnection(_con))
            //{
            //    connection.Open();
            //    using (SQLiteCommand command = new SQLiteCommand("SELECT * FROM TodoTasks WHERE [Id]=id", connection))
            //    {
            //        command.Parameters.AddWithValue("id", id);

            //        using (SQLiteDataReader reader = command.ExecuteReader())
            //        {
            //            if (reader.HasRows)
            //            {
            //                while (reader.Read())
            //                {
            //                    return new Models.TodoTask(int.Parse(reader["Id"].ToString()), reader["Title"].ToString(), reader["Description"].ToString(), reader["Location"].ToString(), reader["AddedDate"].ToString());
            //                }
            //            }
            //            return new Models.TodoTask();
            //        }
            //    }
            //}
        }

        int ILiteDbTodoTaskService.InsertTodoTask(Models.TodoTask todoTask)
        {
            return _db.GetCollection<Models.TodoTask>("Api").Insert(todoTask);
        }

        bool ILiteDbTodoTaskService.UpdateTodoTask(Models.TodoTask todoTask)
        {
            return _db.GetCollection<Models.TodoTask>("Api").Update(todoTask);
        }
    }
}
