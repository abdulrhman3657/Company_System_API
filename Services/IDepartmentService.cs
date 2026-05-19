using Company_System_API.Models;

namespace Company_System_API.Services
{
    public interface IDepartmentService
    {
        List<Department> GetDepartmentsService();
        void AddDepartmentService(Department department);
        Department? GetDepartmentByIdService(int id);
    }
}
