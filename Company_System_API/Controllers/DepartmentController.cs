using Company_System_API.Responses;
using Company_System_Application.Services;
using Company_System_Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService departmentService;
        private readonly ILogger<DepartmentController> logger;

        public DepartmentController(IDepartmentService departmentServiceFromDI, ILogger<DepartmentController> loggerFromDI)
        {
            departmentService = departmentServiceFromDI;
            logger = loggerFromDI;
        }

        // Get all departments
        [Authorize]
        [HttpGet]
        public IActionResult GetDepartments()
        {
            List<Department> Departments = departmentService.GetDepartmentsService();

            logger.LogInformation("Successfully returned {Count} departments", Departments.Count);

            return Ok(new ApiResponse<List<Department>>()
            {
                Data = Departments,
                Message = "Returned all departments",
                Success = true
            });
        }

        // Add new department
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            departmentService.AddDepartmentService(department);

            logger.LogInformation("Successfully added new department, id: {Id}", department.Id);

            return Ok(new ApiResponse<Department>()
            {
                Data = department,
                Message = "Added new department",
                Success = true
            });
        }

        // Get department by id
        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            Department? department = departmentService.GetDepartmentByIdService(id);

            if (department == null)
            {
                logger.LogWarning("failed to return department");

                return NotFound(new ApiResponse<Department>()
                {
                    Data = null,
                    Message = "department not found",
                    Success = false
                });
            }

            logger.LogInformation("Successfully returned department, id: {Id}", department.Id);

            return Ok(new ApiResponse<Department>()
            {
                Data = department,
                Message = "Returned department by id",
                Success = true
            });
        }
    }
}
