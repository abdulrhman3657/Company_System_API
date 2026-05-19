using Company_System_API.Models;

namespace Company_System_API.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();
        void AddEmployee(Employee employee);
        Employee? GetEmployeeById(int id);
    }
}
