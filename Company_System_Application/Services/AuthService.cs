

using Company_System_Infrastructure.Models;
using Company_System_Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Company_System_Application.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
    {
        public User? Register(UserDto request)
        {
            var checkUser = userRepository.CheckUserByUsername(request.Username);

            if (checkUser)
            {
                return null;
            }

            var newUser = new User();

            var hashedPassword = new PasswordHasher<User>().HashPassword(newUser, request.Password);

            newUser.Username = request.Username;
            newUser.PasswordHash = hashedPassword;

            userRepository.AddNewUser(newUser);

            return newUser;
        }

        public TokenResponseDto? Login(UserDto request)
        {
            var user = userRepository.findUserByUsername(request.Username);

            if(user is null)
            {
                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var response = CreateTokenResponse(user);

            return response;
        }

        private TokenResponseDto CreateTokenResponse(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = "" // todo 
            };
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:Token")!)
             );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: configuration.GetValue<string>("AppSettings:Issuer"),
                audience: configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

    }
}
