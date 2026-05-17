using Microsoft.EntityFrameworkCore;

namespace Company_System_API.Models
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
    }
}
