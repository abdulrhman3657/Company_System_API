using Company_System_API.Data;
using Company_System_API.Models;

namespace Company_System_API.Services
{
    public class DepartmentService
    {
        private readonly DB db;

        public DepartmentService(DB dbFromDI)
        {
            db = dbFromDI;
        }

        public List<Department> GetDepartmentsService()
        {

            return db.DepartmentDB.ToList();
        }
        
        public void AddDepartmentService(Department department)
        {
            db.DepartmentDB.Add(department);

            db.SaveChanges();
        }

        public Department? GetDepartmentByIdService(int id)
        {
            Department? department = db.DepartmentDB.Find(id);

            return department;
        }
    }
}
