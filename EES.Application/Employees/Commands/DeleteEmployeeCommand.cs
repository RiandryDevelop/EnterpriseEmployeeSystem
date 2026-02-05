using EES.Application.Common.Interfaces;
using MediatR;

namespace EES.Application.Employees.Commands;

/// <summary>
/// Represents the command to remove an employee from the system.
/// </summary>
/// <param name="Id">The unique identifier of the employee to be deleted.</param>
public record DeleteEmployeeCommand(int Id) : IRequest<bool>;

/// <summary>
/// Handler for the DeleteEmployeeCommand.
/// Implements the logic to safely remove an employee record and persist the change.
/// </summary>
public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, bool>
{
    private readonly IApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the handler with the database context.
    /// </summary>
    /// <param name="context">The application database context contract.</param>
    public DeleteEmployeeHandler(IApplicationDbContext context) => _context = context;

    /// <summary>
    /// Handles the deletion process. 
    /// Verifies existence before removal to ensure data integrity.
    /// </summary>
    /// <returns>True if the record was successfully deleted; otherwise, false.</returns>
    public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        // Retrieve the entity from the database
        var entity = await _context.Employees
            .FindAsync(new object[] { request.Id }, cancellationToken);

        // Return false if the entity does not exist, triggering a 404 response in the API layer
        if (entity == null)
        {
            return false;
        }

        // Remove the entity and persist changes
        _context.Employees.Remove(entity);

        // Return true if at least one row was affected in the database
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}