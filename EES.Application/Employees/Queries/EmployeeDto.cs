namespace EES.Application.Employees.Queries;

/// <summary>
/// Data Transfer Object representing an employee.
/// This class is used to send optimized data to the Frontend (Angular/React).
/// </summary>
public class EmployeeDto
{
    /// <summary>
    /// Unique identifier for the employee record.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Combined First and Last name for display purposes in the UI dashboard.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Professional email address of the employee.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Current job title or position within the company.
    /// </summary>
    public string JobTitle { get; set; } = string.Empty;

    /// <summary>
    /// The date the employee was officially hired.
    /// </summary>
    public DateTime HireDate { get; set; }
}