using EES.Domain.Interfaces;
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
            _logger.LogError(ex, "An unhandled exception occurred."); 
            await emailService.SendAlertAsync(ex.Message);         
            await HandleExceptionAsync(context);
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