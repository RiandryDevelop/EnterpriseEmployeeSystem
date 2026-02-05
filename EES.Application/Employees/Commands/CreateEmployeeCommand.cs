using EES.Application.Common.Interfaces;
using EES.Domain.Entities;
using MediatR;

namespace EES.Application.Employees.Commands;

/// <summary>
/// Represents the command to create a new employee.
/// This record encapsulates all the required fields for the "Enterprise Employee Management System".
/// </summary>
/// <param name="FirstName">The employee's first name.</param>
/// <param name="LastName">The employee's last name.</param>
/// <param name="Email">The employee's professional email address.</param>
/// <param name="JobTitle">The current job title or position.</param>
/// <param name="HireDate">The date the employee was officially hired.</param>
public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string Email,
    string JobTitle,
    DateTime HireDate) : IRequest<int>;

/// <summary>
/// Handler for the CreateEmployeeCommand.
/// Implements the logic to persist a new Employee entity to the database via EF Core.
/// </summary>
public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, int>
{
    private readonly IApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the handler with the database context.
    /// </summary>
    /// <param name="context">The application database context contract.</param>
    public CreateEmployeeHandler(IApplicationDbContext context) => _context = context;

    /// <summary>
    /// Handles the execution of the employee creation logic.
    /// </summary>
    /// <returns>The unique identifier (ID) of the newly created employee.</returns>
    public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // Map the Command data to the Domain Entity
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            JobTitle = request.JobTitle,
            HireDate = request.HireDate
        };

        // Persist the entity to the database using the injected context 
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        // Return the generated database ID
        return employee.Id;
    }
}