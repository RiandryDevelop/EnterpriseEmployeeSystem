namespace EES.Domain.Interfaces;

/// <summary>
/// Defines the contract for the notification service.
/// This service is primarily used to alert the technical team of system-wide exceptions
/// or failed transactions, as required by the technical assessment.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Asynchronously sends an alert message to the pre-configured technical support email.
    /// </summary>
    /// <param name="message">The detailed error or alert message to be transmitted.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task SendAlertAsync(string message);
}