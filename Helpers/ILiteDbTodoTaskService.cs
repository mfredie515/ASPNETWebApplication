using System.Collections.Generic;

namespace ASPNETWebApplication.Helpers
{
    public interface ILiteDbTodoTaskService
    {
        IEnumerable<Models.TodoTask> GetAllTodoTasks();
        Models.TodoTask GetTodoTask(int id);
        int InsertTodoTask(Models.TodoTask todoTask);
        bool UpdateTodoTask(Models.TodoTask todoTask);
        bool DeleteTodoTask(int id);
    }
}