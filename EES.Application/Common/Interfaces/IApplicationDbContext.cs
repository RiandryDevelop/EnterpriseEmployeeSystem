using EES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EES.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    // Employees DbSet
    DbSet<Employee> Employees { get; }

    // SaveChangesAsync method
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}