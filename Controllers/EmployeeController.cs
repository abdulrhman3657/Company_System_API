using Company_System_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Company_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // Get all employees
        [HttpGet]
        public IActionResult GetEmployees(DB db)
        {
            List<Employee> Employees = db.EmployeeDB.ToList();

            return Ok(Employees);
        }

        // Add new employee
        [HttpPost]
        public IActionResult AddEmployee(Employee employee, DB db)
        {
            db.EmployeeDB.Add(employee);

            db.SaveChanges();

            return Ok(employee);
        }

        // Get employee by id
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(DB db, int id)
        {
            Employee? employee = db.EmployeeDB.Find(id);

            if (employee == null)
            {
                return NotFound("employee not found");
            }

            return Ok(employee);
        }
    }
}
