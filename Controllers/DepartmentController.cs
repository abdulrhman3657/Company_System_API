using Company_System_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Company_System_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        // Get all departments
        [HttpGet]
        public IActionResult GetDepartments(DB db)
        {
            List<Department> Departments = db.DepartmentDB.ToList();

            return Ok(Departments);
        }

        // Add new department
        [HttpPost]
        public IActionResult AddDepartment(Department department, DB db)
        {
            db.DepartmentDB.Add(department);

            db.SaveChanges();

            return Ok(department);
        }

        // Get department by id
        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(DB db, int id)
        {
            Department? department = db.DepartmentDB.Find(id);

            if (department == null)
            {
                return NotFound("department not found");
            }

            return Ok(department);
        }
    }
}
