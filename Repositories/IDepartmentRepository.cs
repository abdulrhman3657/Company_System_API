using Company_System_API.Models;

namespace Company_System_API.Repositories
{
    public interface IDepartmentRepository
    {
        List<Department> GetDepartments();
        void AddDepartment(Department department);
        Department? GetDepartmentById(int id);
    }
}
