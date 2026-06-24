

using System.Text.Json.Serialization;

namespace Company_System_Infrastructure.Models
{
    public class Employee
    {
        // SQL Server will automatically generate value for the Id
        // Id is the primary key
        public int Id { set; get; }
        // required forces the Employee objects to have FirstName at initialization
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required decimal Salary { get; set; }

        // Foreign key
        public int DepartmentId { set; get; }

      // navigation property
      // used to get the Department object that is related to the Employee
      [JsonIgnore]
      public Department? Department { get; set; }

    }
}
