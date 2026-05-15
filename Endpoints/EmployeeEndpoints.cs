using Microsoft.EntityFrameworkCore;

namespace Company_System_API.Endpoints
{
    // A static class is used as a container for methods that do not need an object.
    // you cannot create objects for this class
    public static class EmployeeEndpoints
    {
        // you can use this method without creating an object
        // Add this method to the WebApplication type
        // "this" here extends the WebApplication class with MapEmployeeEndpoints method 
        public static void MapEmployeeEndpoints(this WebApplication app)
        {
            // get all employees
            app.MapGet("/employees", async (DB db) =>
            {
                var employees = await db.EmployeeDB.ToListAsync();

                return Results.Ok(new
                {
                    Employees = employees
                });
            });

            // add new employee
            app.MapPost("/employees", async (DB db, Employee employee) =>
            {
                db.EmployeeDB.Add(employee);

                await db.SaveChangesAsync();

                return Results.Ok(employee);
            });

            // get emplyee by id
            app.MapGet("/employees/{id}", async (int id, DB db) =>
            {
                Employee? employee = await db.EmployeeDB.FindAsync(id);

                if(employee == null)
                {
                    return Results.NotFound("Employee not found");
                }

                return Results.Ok(employee);
            });
        }
    }
}
