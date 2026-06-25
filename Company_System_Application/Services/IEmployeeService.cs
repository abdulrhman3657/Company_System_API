using Company_System_Infrastructure.Models;

namespace Company_System_Application.Services
{
    public interface IEmployeeService
    {
        List<EmployeeDto> GetEmployeesService();
        void AddEmployeeService(Employee employee);
        Employee? GetEmployeeByIdService(int id);
    }
}
