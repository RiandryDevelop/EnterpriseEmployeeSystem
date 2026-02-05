namespace EES.Domain.Entities;

/// <summary>
/// Represents the central Employee entity for the enterprise system.
/// This entity corresponds to the 'Employees' table in the SQL database.
/// </summary>
public class Employee
{
    /// <summary>
    /// Primary key for the Employee entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The employee's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// The employee's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// The employee's unique professional email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The current job title or position held by the employee.
    /// </summary>
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// The date the employee joined the organization.
    /// </summary>
    public DateTime HireDate { get; set; }
}