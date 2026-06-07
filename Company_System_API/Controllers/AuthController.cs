using Company_System_API.Responses;
using Company_System_Application.Services;
using Company_System_Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, ILogger<AuthController> logger) : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register(UserDto request)
        {
            logger.LogInformation("Register attempt for username: {Username}", request.Username);

            var newUser = authService.Register(request);

            if (newUser is null)
            {
                logger.LogWarning("Register failed. Username already exists: {Username}", request.Username);

                return BadRequest(new ApiResponse<User>()
                {
                    Data = null,
                    Message = "username already exists",
                    Success = false
                });
            }

            logger.LogInformation("User created successfully. Username: {Username}, Role: {Role}", newUser.Username, newUser.Role);

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
            logger.LogInformation("Login attempt for username: {Username}", request.Username);

            var result = authService.Login(request);

            if (result is null)
            {

                logger.LogWarning("Login failed for username: {Username}", request.Username);

                return BadRequest(new ApiResponse<TokenResponseDto>()
                {
                    Data = null,
                    Message = "wrong username or password",
                    Success = false
                });
            }

            logger.LogInformation("Login successful for username: {Username}", request.Username);

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
            logger.LogInformation("Refresh token attempt for user id: {UserId}", request.UserId);

            var result =  authService.RefreshToken(request);

            if (result is null || result.AccessToken is null || result.RefreshToken is null)
            {
                logger.LogWarning("Refresh token failed for user id: {UserId}", request.UserId);

                return Unauthorized(new ApiResponse<TokenResponseDto>()
                {
                    Data = null,
                    Message = "Invalid refresh token",
                    Success = false
                });
            }

            logger.LogInformation("Refresh token succeeded for user id: {UserId}", request.UserId);

            return Ok(new ApiResponse<TokenResponseDto>()
            {
                Data = result,
                Message = "successfully refreshed token",
                Success = true
            });
        }
    }
}
