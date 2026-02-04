using EES.Application.Common.Interfaces;
using EES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EES.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    // Constructor
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure unique index on Email property of Employee entity
        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();
    }
 
}