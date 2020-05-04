using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETWebApplication.Helpers
{
    public class LiteDbTodoTaskService : ILiteDbTodoTaskService
    {
        private LiteDB.LiteDatabase _db;

        public LiteDbTodoTaskService(ILiteDbContext liteDbContext)
        {
            _db = liteDbContext.Database;
        }

        bool ILiteDbTodoTaskService.DeleteTodoTask(int id)
        {
            return _db.GetCollection<Models.TodoTask>("Api").Delete(id);
        }

        IEnumerable<Models.TodoTask> ILiteDbTodoTaskService.GetAllTodoTasks()
        {
            return _db.GetCollection<Models.TodoTask>("TodoTasks").FindAll();
        }

        Models.TodoTask ILiteDbTodoTaskService.GetTodoTask(int id)
        {
            return _db.GetCollection<Models.TodoTask>("TodoTasks").Find(t => t.Id == id).FirstOrDefault();
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
