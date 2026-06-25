using Company_System_Infrastructure.Data;
using Company_System_Infrastructure.Models;

namespace Company_System_Infrastructure.Repositories
{
  public class EmployeeRepository(DB db) : IEmployeeRepository
  {
    public List<EmployeeDto> GetEmployeesWithDepartment()
    {
      return db.EmployeeDB
        .Select(e => new EmployeeDto
        {
          Id = e.Id,
          FirstName = e.FirstName,
          LastName = e.LastName,
          Salary = e.Salary,
          DepartmentId = e.DepartmentId,
          DepartmentName = e.Department != null ? e.Department.Name : string.Empty
        })
        .ToList();
    }
  }
}
