using EES.Application.Common.Interfaces;
using EES.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EES.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

  //  [cite_start]// Esta es la tabla que pide el reto [cite: 9]
    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración profesional: Email único
        modelBuilder.Entity<Employee>()
            .HasIndex(e => e.Email)
            .IsUnique();
    }
 
}