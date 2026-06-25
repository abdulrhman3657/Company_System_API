using Company_System_Infrastructure.Models;

namespace Company_System_Infrastructure.Repositories
{
  public interface IEmployeeRepository
  {
    List<EmployeeDto> GetEmployeesWithDepartment();
  }
}
