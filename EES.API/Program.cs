using EES.API.Middleware;
using EES.Application.Common.Interfaces;
using EES.Domain.Interfaces;
using EES.Infrastructure.Persistence;
using EES.Infrastructure.Services;
using Microsoft.ApplicationInsights.AspNetCore;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

// Create a builder for the web application
var builder = WebApplication.CreateBuilder(args);

// ------------- Add services to the container. -------------------


// Add controllers to the services collection
builder.Services.AddControllers();

// Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Get the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Dependency Injection for Email Service
builder.Services.AddScoped<IEmailService, EmailAlertService>();
// Dependency Injection for ApplicationDbContext interface
builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());
// Dependency Injection for Employee Repository
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(EES.Application.Employees.Queries.EmployeeDto).Assembly));
// Dependency Injection for FluentValidation validators
builder.Services.AddValidatorsFromAssembly(typeof(EES.Application.Common.Interfaces.IApplicationDbContext).Assembly);
// Configure Application Insights
builder.Services.AddApplicationInsightsTelemetry();

// ------------- Build the application. -------------------
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enforce HTTPS redirection
app.UseHttpsRedirection();
// Enable authorization middleware
app.UseAuthorization();
// Use custom exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();
// Map controller routes
app.MapControllers();

// Run the application
app.Run();
