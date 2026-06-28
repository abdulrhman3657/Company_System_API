

using Company_System_Infrastructure.Models;

namespace Company_System_Application.Services
{
    public interface IAuthService
    {
        User? Register(UserDto user);
        public TokenResponseDto? Login(UserDto request);
        public TokenResponseDto? RefreshToken(RefreshTokenRequestDto request);
        User? EditUser(Guid id, User updatedUser);
        bool DeleteUser(Guid id);
    }
}
