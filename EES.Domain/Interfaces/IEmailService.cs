namespace EES.Domain.Interfaces;

public interface IEmailService
{
    // Method to send alert emails
    Task SendAlertAsync(string message);  
}