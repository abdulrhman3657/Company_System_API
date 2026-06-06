using Company_System_Application.Services;
using Company_System_Infrastructure.Data;
using Company_System_Infrastructure.Models;
using Company_System_Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// add controller support for the app
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// register DB so it is not treated as request body
builder.Services.AddDbContext<DB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["AppSettings:Issuer"],

            ValidateAudience = true,
            ValidAudience = builder.Configuration["AppSettings:Audience"],

            ValidateLifetime = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]!)
            ),

            ValidateIssuerSigningKey = true
        };
    });

// Registers EmployeeService in ASP.NET Core dependency injection
// so controllers can access it
// Scoped means create one EmployeeService object per HTTP request
// When controller asks for IEmployeeService, create a EmployeeService as it
// satisfy IEmployeeService contract
// you can swap this with ex: EmployeeServiceTestData
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddScoped<IUserRepository ,UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// register Department and Employee for the repository
builder.Services.AddScoped<IGenericRepository<Department>, GenericRepository<Department>>();
builder.Services.AddScoped<IGenericRepository<Employee>, GenericRepository<Employee>>();


// register health check
// check for database connectivity
builder.Services.AddHealthChecks().AddDbContextCheck<DB>();

var app = builder.Build();

// Swagger middleware
// check for mode from launchSettings.json
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

// map the health endpoint
app.MapHealthChecks("/health");

// main path
app.MapGet("/", () =>
{
    return Results.Ok(new { response = "Welcome to company API" });
});

// look at all controller classes and connect their routes to the HTTP request pipeline
app.MapControllers();

app.Run();