using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            var list = await _service.GetPaginatedAsync(new PaginationParams { Page = page, Limit = limit });
            return Ok(list);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _service.GetOneByID(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryDTO categoryDTO)
        {
            try
            {
                var response = await _service.AddAsync(categoryDTO);
                return CreatedAtRoute("GetCategoryById", new { response });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO updateCategoryDTO)
        {
            var updated = await _service.UpdateAsync(updateCategoryDTO);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var existing = await _service.GetOneByID(id);
            if (existing == null) return NotFound();

            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
