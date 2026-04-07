using Microsoft.AspNetCore.Mvc;
using Todo_api_backend.DTOs.Auth;
using Todo_api_backend.Interfaces.Services;

namespace Todo_api_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService service)
        {
            _authService = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginDTO dto)
        {
            try
            {
                var response = await _authService.LoginAsync(dto);
                return Ok(new { response });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterDTO dto)
        {
            try
            {
                var response = await _authService.RegisterAsync(dto);
                return CreatedAtAction(null, new { response });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // In a real application, you would handle token invalidation or cookie clearing here.
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
