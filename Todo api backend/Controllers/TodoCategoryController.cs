using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Todo_api_backend.Controllers
{
    [ApiController]
    [Route("todocategory")]
    public class TodoCategoryController : Controller
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateTodoCategory()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId?.Value, out Guid userGuid) == false) return BadRequest();
            return Ok();
        }


        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoCategory(Guid id)
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (Guid.TryParse(userId?.Value, out Guid userGuid) == false) return BadRequest();
            return Ok();
        }
    }
}
