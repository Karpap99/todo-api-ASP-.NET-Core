using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.TodoDtos;
using Todo_api_backend.Interfaces.Services;

namespace Todo_api_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : Controller
    {
        private readonly ITodoService _service;
        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (Guid.TryParse( userId?.Value, out Guid userGuid) == false) 
                return BadRequest();

            var pagination = new PaginationParams { Page = page, Limit = limit };

            var response = await _service.GetPaginatedAsync(pagination, userGuid);

            return Ok(new { response });
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse( userId?.Value, out Guid userGuid) == false ) 
                return BadRequest();

            var response = await _service.GetOneByID(id, userGuid);
            return Ok(new { response });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTodoDTO createTodoDTO)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse( userId?.Value, out Guid userGuid) == false) return BadRequest();
            var response = await _service.AddAsync(createTodoDTO, userGuid);
            return CreatedAtAction(null, new { response }); ;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] UpdateTodoDTO updateTodoDTO)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId?.Value, out Guid userGuid) == false) return BadRequest();
            var response = await _service.UpdateAsync(updateTodoDTO, userGuid);
            return CreatedAtAction(null, new { response }); ;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId?.Value, out Guid userGuid) == false) return BadRequest();
            await _service.DeleteAsync(id, userGuid);
            return Ok("deleted");
        }
    }
}
