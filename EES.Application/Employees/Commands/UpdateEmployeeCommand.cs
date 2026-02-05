using EES.Application.Common.Interfaces;
using MediatR;

namespace EES.Application.Employees.Commands;

/// <summary>
/// Command to update the details of an existing employee.
/// Implements IRequest of bool to signal success or failure to the controller.
/// </summary>
/// <param name="Id">The unique identifier of the employee to update.</param>
/// <param name="FirstName">The updated first name.</param>
/// <param name="LastName">The updated last name.</param>
/// <param name="JobTitle">The updated job title or position.</param>
public record UpdateEmployeeCommand(
    int Id,
    string FirstName,
    string LastName,
    string JobTitle) : IRequest<bool>;

/// <summary>
/// Handler for the UpdateEmployeeCommand.
/// Manages the retrieval, state update, and persistence of the employee entity.
/// </summary>
public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly IApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the handler with the database context contract.
    /// </summary>
    public UpdateEmployeeHandler(IApplicationDbContext context) => _context = context;

    /// <summary>
    /// Handles the update logic by fetching the entity and applying changes.
    /// </summary>
    /// <returns>True if the update was successful and persisted; otherwise, false.</returns>
    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // Fetch the entity using the primary key
        var entity = await _context.Employees
            .FindAsync(new object[] { request.Id }, cancellationToken);

        // If the entity is not found, return false to trigger a 404 response in the API
        if (entity == null)
        {
            return false;
        }

        // Apply updated values to the domain entity
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.JobTitle = request.JobTitle;

        // Persist changes and return true if rows were affected
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}