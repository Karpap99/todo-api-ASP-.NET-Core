using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo_api_backend.DTOs.TodoTask;
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId.Value, out Guid userGuid) == false) return BadRequest();
            var response = await _service.GetAllAsync(userGuid);
            return Ok(new { response });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId.Value, out Guid userGuid) == false) return BadRequest();
            var response = await _service.GetOneByID(id, userGuid);
            return Ok(new { response });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTodoTaskDTO createTodoTaskDTO)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId.Value, out Guid userGuid) == false) return BadRequest();
            var response = await _service.Add(createTodoTaskDTO, userGuid);
            return CreatedAtAction(null, new { response }); ;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTodoTaskDTO updateTodoTaskDTO)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId.Value, out Guid userGuid) == false) return BadRequest();
            var response = await _service.Update(updateTodoTaskDTO, userGuid);
            return CreatedAtAction(null, new { response }); ;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId.Value, out Guid userGuid) == false) return BadRequest();
            await _service.Delete(id, userGuid);
            return Ok("deleted");
        }
    }
}
