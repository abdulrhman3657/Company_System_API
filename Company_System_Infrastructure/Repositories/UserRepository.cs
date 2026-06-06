

using Company_System_Infrastructure.Data;
using Company_System_Infrastructure.Models;

namespace Company_System_Infrastructure.Repositories
{
    public class UserRepository(DB db) : IUserRepository
    {
        public void AddNewUser(User user)
        {
            db.UserDB.Add(user);
            db.SaveChanges();
        }

        public bool CheckUserByUsername(string username)
        {
            return db.UserDB.Any(u => u.Username == username);
        }

        public User? FindUserByUsername(string username)
        {
            var user = db.UserDB.FirstOrDefault(u => u.Username == username);

            return user;
        }

        public User? FindUserById(Guid id)
        {
            var user = db.UserDB.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public void SetRefreshToken(string refreshToken, DateTime RefreshTokenExpiryTime, User user)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = RefreshTokenExpiryTime;

            db.SaveChanges();
        }
    }
}
