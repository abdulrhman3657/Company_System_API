using Company_System_Infrastructure.Models;
using Company_System_Infrastructure.Repositories;

namespace Company_System_Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IGenericRepository<Department> departmentRepository;

        public DepartmentService(IGenericRepository<Department> departmentRepositoryFromDI)
        {
            departmentRepository = departmentRepositoryFromDI;
        }

        public List<Department> GetDepartmentsService()
        {
            return departmentRepository.Get();
        }
        
        public void AddDepartmentService(Department department)
        {
            departmentRepository.Add(department);
        }

        public Department? GetDepartmentByIdService(int id)
        {
            return departmentRepository.GetById(id);
        }
    }
}
