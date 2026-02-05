using EES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EES.Application.Common.Interfaces;

/// <summary>
/// Defines the abstraction for the application database context.
/// This interface allows the Application layer to interact with the database 
/// without being tightly coupled to the Infrastructure implementation.
/// </summary>
public interface IApplicationDbContext
{
    /// <summary>
    /// Gets the collection of Employee entities for CRUD operations.
    /// This supports the "Full management of an Employee entity" requirement.
    /// </summary>
    DbSet<Employee> Employees { get; }

    /// <summary>
    /// Asynchronously persists all changes made in this context to the underlying database.
    /// </summary>
    /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
    /// <returns>A task that represents the asynchronous save operation, containing the number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}