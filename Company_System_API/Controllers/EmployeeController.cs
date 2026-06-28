using Company_System_API.Responses;
using Company_System_Application.Services;
using Company_System_Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Company_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService employeeService;
        private readonly ILogger<EmployeeController> logger;

        public EmployeeController(IEmployeeService employeeServiceFromDI, ILogger<EmployeeController> loggerFromDI)
        {
            employeeService = employeeServiceFromDI;
            logger = loggerFromDI;
        }

        // Get all employees
        [HttpGet]
        public IActionResult GetEmployees()
        {

            List<EmployeeDto> Employees = employeeService.GetEmployeesService();

            logger.LogInformation("Successfully returned {Count} employees", Employees.Count);

            return Ok(new ApiResponse<List<EmployeeDto>>()
            {
                Data = Employees,
                Message = "Returned all employees",
                Success = true
            });
        }

        // Add new employee
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            employeeService.AddEmployeeService(employee);

            logger.LogInformation("Successfully added new employee, id: {Id}", employee.Id);

            return Ok(new ApiResponse<Employee>()
            {
                Data = employee,
                Message = "Added new employee",
                Success = true
            });
        }

        // Get employee by id
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            Employee? employee = employeeService.GetEmployeeByIdService(id);

            if (employee == null)
            {
                logger.LogWarning("failed to return employee");

                return NotFound(new ApiResponse<Employee>()
                {
                    Data = null,
                    Message = "employee not found",
                    Success = false
                });
            }

            logger.LogInformation("Successfully returned employee, id: {Id}", employee.Id);

            return Ok(new ApiResponse<Employee>()
            {
                Data = employee,
                Message = "Returned employee by id",
                Success = true
            });
        }

        [HttpPut("{id}")]
        public IActionResult EditEmployeeById(int id, Employee employee)
        {
            employee.Id = id;

            var updatedEmployee = employeeService.EditEmployee(id, employee);

            if (updatedEmployee == null)
            {
                logger.LogWarning("failed to edit employee");

                return NotFound(new ApiResponse<Employee>()
                {
                    Data = null,
                    Message = "employee not found",
                    Success = false
                });
            }

            logger.LogInformation("Successfully edited employee, id: {Id}", employee.Id);

            return Ok(new ApiResponse<Employee>()
            {
                Data = updatedEmployee,
                Message = "Edited employee by id",
                Success = true
            });

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployeeById(int id)
        {
            var deleted = employeeService.DeleteEmployee(id);

            if (!deleted)
            {
                logger.LogWarning("failed to delete employee");

                return NotFound(new ApiResponse<Employee>()
                {
                    Data = null,
                    Message = "employee not found",
                    Success = false
                });
            }

            logger.LogInformation("Successfully deleted employee, id: {Id}", id);

            return Ok(new ApiResponse<Employee>()
            {
                Data = null,
                Message = "Deleted employee by id",
                Success = true
            });
        }
    }
}
