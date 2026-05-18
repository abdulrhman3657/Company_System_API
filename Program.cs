using Company_System_API.Data;
using Company_System_API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add controller support for the app
builder.Services.AddControllers();

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// register DB so it is not treated as request body
builder.Services.AddDbContext<DB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registers EmployeeService in ASP.NET Core dependency injection
// so controllers can access it
// Scoped means create one EmployeeService object per HTTP request
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<DepartmentService>();

var app = builder.Build();

// Swagger middleware
// check for mode from launchSettings.json
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// main path
app.MapGet("/", () =>
{
    return Results.Ok(new { response = "Welcome to company API" });
});

// look at all controller classes and connect their routes to the HTTP request pipeline
app.MapControllers();

app.Run();