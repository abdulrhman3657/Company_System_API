using Company_System_Infrastructure.Models;
using Company_System_Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace Company_System_Application.Services
{
  public class DepartmentService(
    IGenericRepository<Department> departmentRepository,
    IDepartmentRepository departmentRepositoryWithEmployees,
    ILogger<DepartmentService> logger) : IDepartmentService
  {
    public List<Department> GetDepartmentsService()
      {
        logger.LogInformation("GetDepartmentsService started");

        return departmentRepositoryWithEmployees.GetDepartmentsWithEmployees();
      }

    public void AddDepartmentService(Department department)
        {
            departmentRepository.Add(department);

            logger.LogInformation("successfully added a new department: {DepartmentId}", department.Id);
        }

        public Department? GetDepartmentByIdService(int id)
        {
            logger.LogInformation("GetDepartmentByIdService started, department: {DepartmentId}", id);

            return departmentRepository.GetById(id);
        }
        public Department? EditDepartment(int id, Department department)
        {
            return departmentRepository.Edit(id, department);
        }
        public bool DeleteDepartment(int id)
        {
            return departmentRepository.Delete(id);
        }
    }
}
