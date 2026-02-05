using EES.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace EES.Infrastructure.Notifications;

/**
 * Implementation of the notification logic.
 * Per assessment requirements, this is a mock implementation that logs alerts.
 */
public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;

    public EmailService(ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task SendCriticalErrorAlertAsync(string errorMessage, string stackTrace)
    {
        // Simulation of an email trigger to the technical team
        _logger.LogCritical("--- EMAIL ALERT TRIGGERED ---");
        _logger.LogCritical($"To: tech-team@enterprise.com");
        _logger.LogCritical($"Subject: CRITICAL ERROR 500 - EEMS System");
        _logger.LogCritical($"Message: {errorMessage}");
        _logger.LogCritical($"StackTrace: {stackTrace}");

        return Task.CompletedTask;
    }
}