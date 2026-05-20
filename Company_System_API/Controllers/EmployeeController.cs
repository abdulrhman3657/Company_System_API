using Company_System_Infrastructure.Models;
using Company_System_Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeServiceFromDI)
        {
            employeeService = employeeServiceFromDI;
        }

        // Get all employees
        [HttpGet]
        public IActionResult GetEmployees()
        {

            List <Employee> Employees = employeeService.GetEmployeesService();

            return Ok(Employees);
        }

        // Add new employee
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            employeeService.AddEmployeeService(employee);

            return Ok(employee);
        }

        // Get employee by id
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            Employee? employee = employeeService.GetEmployeeByIdService(id);

            if (employee == null)
            {
                return NotFound("employee not found");
            }

            return Ok(employee);
        }
    }
}
