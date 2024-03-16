using HD.Domain.Departments.Entities;
using HD.Domain.Tickets.Entities;
using HD.Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HD.Infra.Context;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.SetColumnType("varchar(150)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedIn") != null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("CreatedIn").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property("CreatedIn").IsModified = false;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
