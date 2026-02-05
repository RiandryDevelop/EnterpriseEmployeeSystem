using EES.Application.Common.Interfaces;
using EES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EES.Infrastructure.Persistence;

/// <summary>
/// The primary database context for the application.
/// Implements IApplicationDbContext to provide a decoupled interface for the Application layer.
/// This context manages the persistence of Employee data to SQL Server.
/// </summary>
public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    /// <summary>
    /// Gets the Employees table set.
    /// Uses the 'Set<T>' method to ensure a non-null reference in .NET 8.
    /// </summary>
    public DbSet<Employee> Employees => Set<Employee>();

    /// <summary>
    /// Configures the database schema and entity constraints.
    /// Includes unique indexing and field length limitations.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Employee Entity Configuration ---
        modelBuilder.Entity<Employee>(entity =>
        {
            // Ensure the Email is unique at the database level to prevent duplicate registrations
            entity.HasIndex(e => e.Email)
                  .IsUnique();

            // Set explicit column lengths to match business rules and optimize storage
            entity.Property(e => e.FirstName)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(e => e.LastName)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.Property(e => e.JobTitle)
                  .IsRequired()
                  .HasMaxLength(100);
        });
    }
}