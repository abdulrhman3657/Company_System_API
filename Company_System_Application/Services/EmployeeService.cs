using Company_System_Infrastructure.Models;
using Company_System_Infrastructure.Repositories;

namespace Company_System_Application.Services
{
    public class EmployeeService : IEmployeeService
    {

        private readonly IGenericRepository<Employee> employeeRepository;

        public EmployeeService(IGenericRepository<Employee> employeeRepositoryFromDI)
        {
            employeeRepository = employeeRepositoryFromDI;
        }

        public List<Employee> GetEmployeesService()
        {
            return employeeRepository.Get();
        }

        public void AddEmployeeService(Employee employee)
        {
            employeeRepository.Add(employee);
        }

        public Employee? GetEmployeeByIdService(int id)
        {
            return employeeRepository.GetById(id);
        }

    }
}
