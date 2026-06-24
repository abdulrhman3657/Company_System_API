using Company_System_Infrastructure.Data;
using Company_System_Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Company_System_Infrastructure.Repositories
{
  public class DepartmentRepository(DB db) : IDepartmentRepository
  {
    public List<Department> GetDepartmentsWithEmployees()
    {
      return db.DepartmentDB.Include(d => d.Employees).ToList();
    }
  }
}
