

using Company_System_Infrastructure.Models;
using Company_System_Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Company_System_Application.Services
{
    public class AuthService(IUserRepository userRepository, IConfiguration configuration, ILogger<AuthService> logger) : IAuthService
    {
        public User? Register(UserDto request)
        {
            logger.LogInformation("Register service started for username: {Username}", request.Username);

            // check of the user already exists
            var checkUser = userRepository.CheckUserByUsername(request.Username);

            if (checkUser)
            {
                logger.LogWarning("Register service failed. Username already exists: {Username}", request.Username);

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

            logger.LogInformation("Register service completed. User created with id: {UserId}, role: {Role}", newUser.Id, newUser.Role);

            // return the user to the controller
            return newUser;
        }

        public TokenResponseDto? Login(UserDto request)
        {
            logger.LogInformation("Login service started for username: {Username}", request.Username);

            var user = userRepository.FindUserByUsername(request.Username);

            if(user is null)
            {
                logger.LogWarning("Login service failed. Username not found: {Username}", request.Username);

                return null;
            }

            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                logger.LogWarning("Login service failed. Invalid password for username: {Username}", request.Username);

                return null;
            }

            var response = CreateTokenResponse(user);

            logger.LogInformation("Login service completed successfully for user id: {UserId}", user.Id);

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

        // generate the refresh token
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
                if (user == null)
                {
                    logger.LogWarning("Refresh token validation failed. User not found. UserId: {UserId}", userId);
                }
                else if (user.RefreshToken != refreshToken)
                {
                    logger.LogWarning("Refresh token validation failed. Token mismatch for user id: {UserId}", userId);
                }
                 else if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                {
                    logger.LogWarning("Refresh token expired for user id: {UserId}", userId);
                }

                return null;
            }

            logger.LogInformation("Refresh token validation succeeded for user id: {UserId}", userId);

            return user;
        }

        // 
        public TokenResponseDto? RefreshToken(RefreshTokenRequestDto request)
        {
            logger.LogInformation("Refresh token process started for user id: {UserId}", request.UserId);

            var user = ValidateRefreshToken(request.UserId, request.RefreshToken);

            if (user is null)
            {
                logger.LogWarning("Refresh token process failed for user id: {UserId}", request.UserId);

                return null;
            }

            logger.LogInformation("Refresh token process succeeded for user id: {UserId}", request.UserId);

            return CreateTokenResponse(user);
        }

        public User? EditUser(Guid id, User updatedUser)
        {
            return userRepository.EditUser(id, updatedUser);
        }

        public bool DeleteUser(Guid id)
        {
            return userRepository.DeleteUser(id);
        }
    }
}
