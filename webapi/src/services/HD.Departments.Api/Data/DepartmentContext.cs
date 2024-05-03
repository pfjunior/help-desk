using HD.Departments.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HD.Departments.Api.Data;

public class DepartmentContext : DbContext
{
    public DepartmentContext(DbContextOptions<DepartmentContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Department> Departments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.SetColumnType("varchar(100)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DepartmentContext).Assembly);

        modelBuilder.Entity<Department>().HasIndex(x => x.Code);
        modelBuilder.Entity<Department>().Property(x => x.Code).HasColumnType("varchar(8)");
        modelBuilder.Entity<Department>().Property(x => x.Name).HasColumnType("varchar(60)");


        base.OnModelCreating(modelBuilder);
    }
}
