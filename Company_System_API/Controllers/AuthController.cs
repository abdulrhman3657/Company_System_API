using Company_System_API.Responses;
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
                return BadRequest(new ApiResponse<User>()
                {
                    Data = null,
                    Message = "username already exists",
                    Success = false
                });
            }

            return Ok(new ApiResponse<User>()
            {
                Data = newUser,
                Message = "user created",
                Success = true
            });
        }

        [HttpPost("login")]
        public IActionResult Login(UserDto request)
        {
            var result = authService.Login(request);

            if (result is null)
            {
                return BadRequest(new ApiResponse<TokenResponseDto>()
                {
                    Data = null,
                    Message = "wrong username or password",
                    Success = false
                });
            }

            return Ok(new ApiResponse<TokenResponseDto>()
            {
                Data = result,
                Message = "successfully logged in",
                Success = true
            });
        }

        //  this endpoint is used when an old access token expires
        [HttpPost("refresh-token")]
        public IActionResult RefreshToken(RefreshTokenRequestDto request)
        {
            var result =  authService.RefreshToken(request);

            if (result is null || result.AccessToken is null || result.RefreshToken is null)
            {
                return Unauthorized(new ApiResponse<TokenResponseDto>()
                {
                    Data = null,
                    Message = "Invalid refresh token",
                    Success = false
                });
            }

            return Ok(new ApiResponse<TokenResponseDto>()
            {
                Data = result,
                Message = "successfully refreshed token",
                Success = true
            });
        }
    }
}
