using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Todo_api_backend.DTOs;
using Todo_api_backend.DTOs.Category;
using Todo_api_backend.Interfaces.Services;

namespace Todo_api_backend.Controllers
{
    [ApiController]
    [Route("category")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int limit = 10, [FromQuery] string? query = null)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(userId?.Value, out Guid userGuid) == false)
                return BadRequest();

            var response = await _service.GetPaginatedAsync(new PaginationParams { Page = page, Limit = limit }, userGuid);
            return Ok(new { response });
        }


        [Authorize]
        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(userId?.Value, out Guid userGuid) == false)
                return BadRequest();

            var category = await _service.GetOneByID(id, userGuid);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryDTO categoryDTO)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                if (Guid.TryParse(userId?.Value, out Guid userGuid) == false)
                    return BadRequest();

                var response = await _service.AddAsync(categoryDTO, userGuid);
                return CreatedAtRoute("GetCategoryById", new { id = response.Id }, response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(userId?.Value, out Guid userGuid) == false)
                return BadRequest();

            var updated = await _service.UpdateAsync(updateCategoryDTO, userGuid);
            return Ok(updated);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (Guid.TryParse(userId?.Value, out Guid userGuid) == false)
                return BadRequest();
            var existing = await _service.GetOneByID(id, userGuid);
            if (existing == null) return NotFound();

            await _service.DeleteAsync(id, userGuid);
            return NoContent();
        }
    }
}
