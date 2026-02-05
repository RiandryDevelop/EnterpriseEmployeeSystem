using EES.API.Middleware;
using EES.Application.Common.Behaviors;
using EES.Application.Common.Interfaces;
using EES.Domain.Interfaces;
using EES.Infrastructure.Persistence;
using EES.Infrastructure.Services;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

// Initialize the web application builder
var builder = WebApplication.CreateBuilder(args);

// -----------------------------------------------------------
// 1. SERVICE CONFIGURATION (Dependency Injection)
// -----------------------------------------------------------

// Register Controller services to handle incoming HTTP requests [cite: 8]
builder.Services.AddControllers();

// Configure Swagger/OpenAPI for API documentation and testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Data Persistence using EF Core and SQL Server 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Register Application Interfaces for Dependency Injection
builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());

// Register the Notification Service (Email Alert System) [cite: 12, 13]
builder.Services.AddScoped<IEmailService, EmailAlertService>();

// Configure MediatR for Command/Query separation
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(EES.Application.Employees.Queries.EmployeeDto).Assembly));

// Register FluentValidation validators from the Application layer
builder.Services.AddValidatorsFromAssembly(typeof(EES.Application.Common.Interfaces.IApplicationDbContext).Assembly);

// Register Cross-Cutting Concerns: Validation Pipeline Behavior
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Integrate Azure Application Insights for Telemetry and Observability 
builder.Services.AddApplicationInsightsTelemetry();

// Configure CORS Policy to enable Frontend integration (Angular/React) [cite: 4, 14]
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// -----------------------------------------------------------
// 2. HTTP REQUEST PIPELINE (Middleware)
// -----------------------------------------------------------
var app = builder.Build();

// Enable Swagger UI only in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Global Exception Handling Middleware (First in the pipeline to catch all errors) 
app.UseMiddleware<ExceptionMiddleware>();

// Standard ASP.NET Core Middleware
app.UseHttpsRedirection();
app.UseCors("AllowAll"); // Essential for Frontend-Backend communication
app.UseAuthorization();

// Route mapping for Controllers
app.MapControllers();

// Execute the application
app.Run();