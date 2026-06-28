using Company_System_Infrastructure.Models;
using Company_System_Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Company_System_Application.Services
{
    public class EmployeeService(
        IGenericRepository<Employee> employeeRepository,
        IEmployeeRepository employeeRepositoryWithDepartment,
        ILogger<EmployeeService> logger) : IEmployeeService
    {

        public List<EmployeeDto> GetEmployeesService()
        {
            logger.LogInformation("GetEmployeesService started");

            return employeeRepositoryWithDepartment.GetEmployeesWithDepartment();
        }

        public void AddEmployeeService(Employee employee)
        {
            employeeRepository.Add(employee);

            logger.LogInformation("successfully added a new employee: {EmployeeId}", employee.Id);
        }

        public Employee? GetEmployeeByIdService(int id)
        {
            logger.LogInformation("GetEmployeeByIdService started, employee: {EmployeeId}", id);

            return employeeRepository.GetById(id);
        }
        
        public Employee? EditEmployee(int id, Employee employee)
        {
            return employeeRepository.Edit(id, employee);
        }

        public bool DeleteEmployee(int id)
        {
            return employeeRepository.Delete(id);
        }

    }
}
