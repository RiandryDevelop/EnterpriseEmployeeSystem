using EES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EES.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    // DbSet permite realizar consultas LINQ que EF traduce a SQL
    DbSet<Employee> Employees { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}