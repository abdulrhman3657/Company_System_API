using Company_System_API.Data;
using Company_System_API.Models;

namespace Company_System_API.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly DB db;

        public DepartmentRepository(DB dbFromDI)
        {
            db = dbFromDI;
        }

        public List<Department> GetDepartments()
        {

            return db.DepartmentDB.ToList();
        }

        public void AddDepartment(Department department)
        {
            db.DepartmentDB.Add(department);

            db.SaveChanges();
        }

        public Department? GetDepartmentById(int id)
        {
            Department? department = db.DepartmentDB.Find(id);

            return department;
        }
    }
}
