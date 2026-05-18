using Company_System_API.Data;
using Company_System_API.Models;

namespace Company_System_API.Services
{
    public class EmployeeService
    {

        // readonly means that this varviable is set in the constructor and can not be changed later
        private readonly DB db;

        public EmployeeService(DB dbFromDI)
        {
            db = dbFromDI;
        }

        public List<Employee> GetEmployeesService()
        {

            return db.EmployeeDB.ToList();
        }

        public void AddEmployeeService(Employee employee)
        {
            db.EmployeeDB.Add(employee);

            db.SaveChanges();
        }

        public Employee? GetEmployeeByIdService(int id)
        {
            Employee? employee = db.EmployeeDB.Find(id);

            return employee;
        }

    }
}
