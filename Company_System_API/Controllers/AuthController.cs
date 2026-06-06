using Company_System_Application.Services;
using Company_System_Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register(UserDto request)
        {
            var newUser = authService.Register(request);

            if(newUser is null)
            {
                return BadRequest("usersname already exists");
            }

            return Ok(newUser);
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto request)
        {
            var result = authService.Login(request);

            if (result is null)
            {
                return BadRequest("wrong username or password");
            }

            return Ok(result);
        }
    }
}
