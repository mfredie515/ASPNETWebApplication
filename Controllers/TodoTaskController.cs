using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoTaskController : ControllerBase
    {
        private readonly Helpers.ILiteDbTodoTaskService liteDbTodoTaskService;

        public TodoTaskController(Helpers.ILiteDbTodoTaskService liteDbTodoTaskService)
        {
            this.liteDbTodoTaskService = liteDbTodoTaskService;
        }

        [HttpGet]
        public IEnumerable<Models.TodoTask> Get()
        {
            return liteDbTodoTaskService.GetAllTodoTasks();
        }

        [HttpGet("{id}", Name = "GetTodoTask")]
        public ActionResult<Models.TodoTask> Get(int id)
        {
            Models.TodoTask todoTask = liteDbTodoTaskService.GetTodoTask(id);

            if (todoTask != default)
                return Ok(todoTask);
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<Models.TodoTask> Insert(Models.TodoTask todoTask)
        {
            int id = liteDbTodoTaskService.InsertTodoTask(todoTask);
            if (id != default)
                return CreatedAtAction("GetTodoTask", liteDbTodoTaskService.GetTodoTask(id));
            else
                return BadRequest();
        }

        [HttpPut]
        public ActionResult<Models.TodoTask> Update(Models.TodoTask todoTask)
        {
            bool result = liteDbTodoTaskService.UpdateTodoTask(todoTask);
            if (result)
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult<Models.TodoTask> Delete(int id)
        {
            bool result = liteDbTodoTaskService.DeleteTodoTask(id);
            if (result)
                return NoContent();
            else
                return NotFound();
        }
    }
}