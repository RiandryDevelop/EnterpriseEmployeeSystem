using EES.Application.Common.Interfaces;
using EES.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EES.Application.Employees.Queries;

/// <summary>
/// Query request for retrieving a paginated and filtered list of employees.
/// Supports server-side search by Name or Email to fulfill dashboard requirements.
/// </summary>
public record GetEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchTerm { get; init; }
}

/// <summary>
/// Handler for GetEmployeesWithPaginationQuery.
/// Optimized with AsNoTracking for read-only performance in high-traffic dashboards.
/// </summary>
public class GetEmployeesWithPaginationHandler : IRequestHandler<GetEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeesWithPaginationHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Executes the retrieval logic, applying search filters and pagination.
    /// </summary>
    public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        // Start with a non-tracking query to improve performance for read operations
        var query = _context.Employees.AsNoTracking();

        // Apply server-side search filter if a SearchTerm is provided
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(e => e.FirstName.Contains(request.SearchTerm) ||
                                     e.LastName.Contains(request.SearchTerm) ||
                                     e.Email.Contains(request.SearchTerm));
        }

        // Get the total count for pagination metadata before applying Skip/Take
        var count = await query.CountAsync(cancellationToken);

        // Fetch the specific page of data and map directly to the DTO
        var items = await query
            .OrderBy(e => e.LastName) // Default sorting by LastName
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FullName = $"{e.FirstName} {e.LastName}",
                Email = e.Email,
                JobTitle = e.JobTitle,
                HireDate = e.HireDate
            })
            .ToListAsync(cancellationToken);

        // Return the standardized paginated response
        return new PaginatedList<EmployeeDto>(items, count, request.PageNumber, request.PageSize);
    }
}