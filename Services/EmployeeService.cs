using Company_System_API.Models;
using Company_System_API.Repositories;

namespace Company_System_API.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepositoryFromDI)
        {
            employeeRepository = employeeRepositoryFromDI;
        }

        public List<Employee> GetEmployeesService()
        {
            return employeeRepository.GetEmployees();
        }

        public void AddEmployeeService(Employee employee)
        {
            employeeRepository.AddEmployee(employee);
        }

        public Employee? GetEmployeeByIdService(int id)
        {
            return employeeRepository.GetEmployeeById(id);
        }

    }
}
