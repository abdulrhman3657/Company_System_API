using Company_System_Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Company_System_Infrastructure.Data
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options)
        {

        }

        // DbContextOptions

        // create tables
        public DbSet<Employee> EmployeeDB { set; get; }
        public DbSet<Department> DepartmentDB { set; get; }
        public DbSet<User> UserDB { set; get; }
    }
}
