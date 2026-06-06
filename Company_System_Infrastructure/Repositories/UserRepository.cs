

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

        public User? findUserByUsername(string username)
        {
            var user = db.UserDB.FirstOrDefault(u => u.Username == username);

            return user;
        }
    }
}
