using EES.Domain.Interfaces;
using System;
using System.Net;

namespace EES.API.Middleware;

public class ExceptionMiddleware
{
    // Fields
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    // Constructor
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    // InvokeAsync method
    public async Task InvokeAsync(HttpContext context, IEmailService emailService)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // 1. Handle Validation Errors (Don't alert tech team)
            if (ex is FluentValidation.ValidationException valEx)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                var errors = valEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });
                await context.Response.WriteAsJsonAsync(new { Title = "Validation Error", Errors = errors });
            }
            // 2. Handle System Errors (Trigger Email Alert)
            else
            {
                await emailService.SendAlertAsync(ex.Message); // Only send alert for real 500 errors [cite: 12]
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { Title = "Internal Server Error", Message = "A technical error occurred." });
            }
        }
    }

    // HandleExceptionAsync method
    private static Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return context.Response.WriteAsync("Internal Server Error from Global Middleware.");
    }
}