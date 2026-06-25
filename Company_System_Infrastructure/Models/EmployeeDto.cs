namespace Company_System_Infrastructure.Models
{
  public class EmployeeDto
  {
    public int Id { get; set; }
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public decimal Salary { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = "";
  }
}
