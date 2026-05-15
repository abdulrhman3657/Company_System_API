namespace Company_System_API
{
    public class Department
    {
        public int Id { set; get; }
        public required string Name { get; set; }

        // Navigation property
        // a department can have a list of employees
        // one department can have many employees (one to many)
        // this is not stored in the Department table
        // this is a C# navigation property that EF Core fills from the Employee table
        public List<Employee> Employees { get; set; } = new List<Employee>();

    }
}