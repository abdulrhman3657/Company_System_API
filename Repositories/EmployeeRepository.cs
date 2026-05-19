using Company_System_API.Data;
using Company_System_API.Models;

namespace Company_System_API.Repositories
{    
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DB db;

        public EmployeeRepository(DB dbFromDI)
        {
            db = dbFromDI;
        }

        public List<Employee> GetEmployees()
        {
            return db.EmployeeDB.ToList();
        }

        public void AddEmployee(Employee employee)
        {
            db.EmployeeDB.Add(employee);

            db.SaveChanges();
        }

        public Employee? GetEmployeeById(int id)
        {
            Employee? employee = db.EmployeeDB.Find(id);

            return employee;
        }
    }
}
