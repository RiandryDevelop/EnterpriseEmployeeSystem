using EES.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EES.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    ISet<Employee> Employees { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}