using EES.Domain.Interfaces;
using System.Net;
using FluentValidation;

namespace EES.API.Middleware;

/// <summary>
/// Global Exception Handler to intercept all unhandled exceptions within the HTTP request pipeline.
/// This fulfills the requirement for an "Email Alert" system on 500 errors. [cite: 12]
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Processes the request and catches exceptions to provide a unified error response.
    /// </summary>
    public async Task InvokeAsync(HttpContext context, IEmailService emailService)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException valEx)
        {
            // Handle Validation Errors (400 Bad Request)
            // These are client-side errors and do not trigger technical team alerts.
            _logger.LogWarning("Validation failed: {Errors}", valEx.Errors);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            var errors = valEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Validation Error",
                Status = StatusCodes.Status400BadRequest,
                Errors = errors
            });
        }
        catch (Exception ex)
        {
            // Handle System Errors (500 Internal Server Error)
            // Critical failures trigger an "Email Alert" to the technical team. [cite: 12]
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);

            // Mandatory Notification Logic for failed transactions or server errors [cite: 12]
            await emailService.SendAlertAsync($"Global Exception Caught: {ex.Message}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(new
            {
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Message = "A technical error occurred. The support team has been notified."
            });
        }
    }
}