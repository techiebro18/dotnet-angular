using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;
using RestAPI.Services;

namespace RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoRepository _toDoService;

        public ToDoController(IToDoRepository toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet("")]
        public IActionResult GetToDoList()
        {
            var result = _toDoService.ToDos();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetToDoById([FromRoute] int id)
        {
            var obj = _toDoService.GetToDoById(id);
            return Ok(obj);
        }

        [HttpPost("")]
        public IActionResult AddNewTask([FromBody] ToDoModel todoModel)
        {
            var affectedRows = _toDoService.AddNewTask(todoModel);
            return Ok(affectedRows);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask([FromBody] ToDoModel todoModel, [FromRoute] int id)
        {
            var affectedRows = _toDoService.UpdateTask(todoModel, id);
            return Ok(affectedRows);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask([FromRoute] int id)
        {
            var affectedRows = _toDoService.DeleteTask(id);
            return Ok(affectedRows);
        }
    }
}
