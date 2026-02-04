using EES.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace EES.Infrastructure.Services;

public class EmailAlertService : IEmailService
{
    // Logger field
    private readonly ILogger<EmailAlertService> _logger;
    public EmailAlertService(ILogger<EmailAlertService> logger) => _logger = logger;


    // SendAlertAsync method
    public async Task SendAlertAsync(string message)
    {
        _logger.LogCritical("ALERT SENT TO TECH TEAM: {Message}", message);
        await Task.CompletedTask;
    }
}