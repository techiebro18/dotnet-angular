using Microsoft.AspNetCore.Mvc;
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
    }
}
