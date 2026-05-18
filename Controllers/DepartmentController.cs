using Company_System_API.Models;
using Company_System_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService departmentService;

        public DepartmentController(DepartmentService departmentServiceFromDI)
        {
            departmentService = departmentServiceFromDI;
        }

        // Get all departments
        [HttpGet]
        public IActionResult GetDepartments()
        {
            List<Department> Departments = departmentService.GetDepartmentsService();

            return Ok(Departments);
        }

        // Add new department
        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            departmentService.AddDepartmentService(department);

            return Ok(department);
        }

        // Get department by id
        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            Department? department = departmentService.GetDepartmentByIdService(id);

            if (department == null)
            {
                return NotFound("department not found");
            }

            return Ok(department);
        }
    }
}
