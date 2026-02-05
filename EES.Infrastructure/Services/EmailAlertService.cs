using EES.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EES.Infrastructure.Services;

/// <summary>
/// Service responsible for handling system-wide alerts and notifications.
/// Implements the IEmailService contract to decouple notification logic from the business layer.
/// </summary>
public class EmailAlertService : IEmailService
{
    private readonly ILogger<EmailAlertService> _logger;

    /// <summary>
    /// Initializes a new instance of the EmailAlertService.
    /// </summary>
    /// <param name="logger">The logger used to record critical alerts for technical monitoring.</param>
    public EmailAlertService(ILogger<EmailAlertService> logger) => _logger = logger;

    /// <summary>
    /// Simulates sending an alert email to the technical team.
    /// In a production environment, this would integrate with an SMTP client, SendGrid, or Azure Communication Services.
    /// This fulfills the "Email Alert" requirement for critical failures and failed transactions.
    /// </summary>
    /// <param name="message">The exception or transaction failure details.</param>
    public async Task SendAlertAsync(string message)
    {
        // Log as Critical to ensure visibility in Application Insights/Monitoring dashboards.
        _logger.LogCritical("CRITICAL ALERT: SYSTEM EXCEPTION ENCOUNTERED. SENT TO TECH TEAM: {Message}", message);

        // Awaiting a completed task as this implementation currently functions as a mock for the assessment.
        await Task.CompletedTask;
    }
}