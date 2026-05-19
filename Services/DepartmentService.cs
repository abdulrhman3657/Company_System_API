using Company_System_API.Models;
using Company_System_API.Repositories;

namespace Company_System_API.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepositoryFromDI)
        {
            departmentRepository = departmentRepositoryFromDI;
        }

        public List<Department> GetDepartmentsService()
        {
            return departmentRepository.GetDepartments();
        }
        
        public void AddDepartmentService(Department department)
        {
            departmentRepository.AddDepartment(department);
        }

        public Department? GetDepartmentByIdService(int id)
        {
            return departmentRepository.GetDepartmentById(id);
        }
    }
}
