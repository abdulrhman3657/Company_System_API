using Company_System_Infrastructure.Models;

namespace Company_System_Application.Services
{
    public interface IDepartmentService
    {
        List<Department> GetDepartmentsService();
        void AddDepartmentService(Department department);
        Department? GetDepartmentByIdService(int id);
    }
}
