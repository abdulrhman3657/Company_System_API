using Company_System_API;
using Company_System_API.Endpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// register DB so it is not treated as request body
builder.Services.AddDbContext<DB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// employee router
app.MapEmployeeEndpoints();
// department router
app.MapDepartmentEndpoints();

app.Run();