using Company_System_Infrastructure.Models;
using Company_System_Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Company_System_Application.Services
{
    public class EmployeeService(IGenericRepository<Employee> employeeRepository, ILogger<EmployeeService> logger) : IEmployeeService
    {

        public List<Employee> GetEmployeesService()
        {
            logger.LogInformation("GetEmployeesService started");

            return employeeRepository.Get();
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

    }
}
