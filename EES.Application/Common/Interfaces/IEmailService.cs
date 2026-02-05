namespace EES.Application.Common.Interfaces;

/**
 * Interface for the notification system.
 * Defines the contract for sending alerts to the technical team.
 */
public interface IEmailService
{
    Task SendCriticalErrorAlertAsync(string errorMessage, string stackTrace);
}