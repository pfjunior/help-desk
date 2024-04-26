using Microsoft.EntityFrameworkCore;

namespace HD.Users.Api.Data;

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.SetColumnType("varchar(150)");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);

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
                entry.Property("UpdatedIn").IsModified = false;
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
