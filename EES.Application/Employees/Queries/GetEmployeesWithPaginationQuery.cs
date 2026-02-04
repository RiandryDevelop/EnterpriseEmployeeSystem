using EES.Application.Common.Interfaces;
using EES.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EES.Application.Employees.Queries;

// La petición (Request)
public record GetEmployeesWithPaginationQuery : IRequest<PaginatedList<EmployeeDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? SearchTerm { get; init; }
}

// El ejecutor (Handler)
public class GetEmployeesWithPaginationHandler : IRequestHandler<GetEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>
{
    private readonly IApplicationDbContext _context;

    public GetEmployeesWithPaginationHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<EmployeeDto>> Handle(GetEmployeesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Employees.AsNoTracking();

        // Aplicar filtro si existe
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(e => e.FirstName.Contains(request.SearchTerm) ||
                                     e.LastName.Contains(request.SearchTerm) ||
                                     e.Email.Contains(request.SearchTerm));
        }

        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(e => e.LastName)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(e => new EmployeeDto // Mapeo manual (o puedes usar AutoMapper más adelante)
            {
                Id = e.Id,
                FullName = $"{e.FirstName} {e.LastName}",
                Email = e.Email,
                JobTitle = e.JobTitle,
                HireDate = e.HireDate
            })
            .ToListAsync(cancellationToken);

        return new PaginatedList<EmployeeDto>(items, count, request.PageNumber, request.PageSize);
    }
}