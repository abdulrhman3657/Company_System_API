using Company_System_API.Models;

namespace Company_System_API.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployeesService();
        void AddEmployeeService(Employee employee);
        Employee? GetEmployeeByIdService(int id);
    }
}
