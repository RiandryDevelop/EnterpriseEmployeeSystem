namespace EES.Domain.Interfaces;

public interface IEmailService
{
    Task SendAlertAsync(string message); // 
}