using Microsoft.EntityFrameworkCore;

namespace Company_System_API.Endpoints
{
    public static class DepartmentEndpoints
    {
        public static void MapDepartmentEndpoints(this WebApplication app)
        {
            // get all departments
            app.MapGet("/departments", async (DB db) =>
            {
                List<Department> Departments = await db.DepartmentDB.ToListAsync();

                return Results.Ok( new
                {
                    Departments = Departments
                });
            });

            // add new department
            app.MapPost("/departments", async (DB db, Department departments) =>
            {
                db.DepartmentDB.Add(departments);

                await db.SaveChangesAsync();

                return Results.Ok(departments);
            });

            // get department by id
            app.MapGet("/department/{id}", (int id, DB db) =>
            {
                Department? department = db.DepartmentDB.Find(id);

                if (department == null)
                {
                    return Results.NotFound("department not found");
                }

                return Results.Ok(department);
            });
        }
    }
}
