using Company_System_Infrastructure.Models;

namespace Company_System_Infrastructure.Repositories
{
    public interface IUserRepository
    {
        public void AddNewUser(User user);
        public bool CheckUserByUsername(string username);
        public User? FindUserByUsername(string username);
        public User? FindUserById(Guid id);
        public void SetRefreshToken(string refreshToken, DateTime RefreshTokenExpiryTime, User user);
    }
}
