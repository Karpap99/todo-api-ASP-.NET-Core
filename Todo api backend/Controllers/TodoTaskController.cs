using Microsoft.AspNetCore.Mvc;
using Todo_api_backend.Interfaces.Services;

namespace Todo_api_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoTaskController : Controller
    {
        private readonly ITodoTaskService _service;
        public TodoTaskController(ITodoTaskService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _service.GetAllAsync().Result;
            return Ok("Hello from TodoTaskController!");

        }
    }
}
