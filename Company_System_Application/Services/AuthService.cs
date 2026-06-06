

using Company_System_Infrastructure.Models;
using Company_System_Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Company_System_Application.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration) : IAuthService
    {
        public User? Register(UserDto request)
        {
            // check of the user already exists
            var checkUser = userRepository.CheckUserByUsername(request.Username);

            if (checkUser)
            {
                return null;
            }

            // create a new user object
            var newUser = new User();

            var hashedPassword = new PasswordHasher<User>().HashPassword(newUser, request.Password);

            newUser.Username = request.Username;
            newUser.PasswordHash = hashedPassword;
            newUser.Role = "Employee";

            // save the new user to the db
            userRepository.AddNewUser(newUser);

            // return the user to the controller
            return newUser;
        }

        public TokenResponseDto? Login(UserDto request)
        {
            var user = userRepository.FindUserByUsername(request.Username);

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

        // helper method
        // create the full login/refresh response
        private TokenResponseDto CreateTokenResponse(User user)
        {
            return new TokenResponseDto
            {
                AccessToken = CreateToken(user),
                RefreshToken = GenerateAndSaveRefreshToken(user)
            };
        }

        private string GenerateAndSaveRefreshToken(User user)
        {
            var refreshToken = GenerateRefreshToken();

            userRepository.SetRefreshToken(refreshToken, DateTime.UtcNow.AddDays(7), user);

            return refreshToken;
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

        // generare the refresh token
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        private User? ValidateRefreshToken(Guid userId, string refreshToken)
        {
            // find user by id
            var user = userRepository.FindUserById(userId);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            return user;
        }

        // 
        public TokenResponseDto? RefreshToken(RefreshTokenRequestDto request)
        {
            var user = ValidateRefreshToken(request.UserId, request.RefreshToken);

            if (user is null)
            {
                return null;
            }

            return CreateTokenResponse(user);
        }
    }
}
