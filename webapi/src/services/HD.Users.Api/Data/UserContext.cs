using FluentValidation.Results;
using HD.Core.Data;
using HD.Core.Mediator;
using HD.Core.Messages;
using HD.Users.Api.Data.Extension;
using HD.Users.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HD.Users.Api.Data;

public class UserContext : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;

    public UserContext(DbContextOptions<UserContext> options, IMediatorHandler mediatorHandler) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        ChangeTracker.AutoDetectChangesEnabled = false;
        _mediatorHandler = mediatorHandler;
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.SetColumnType("varchar(100)");
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);

        modelBuilder.Ignore<ValidationResult>();
        modelBuilder.Ignore<Event>();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("CreatedAt").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property("UpdatedAt").IsModified = false;
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> CommitAsync()
    {
        var success = await SaveChangesAsync() > 0;

        if (success) await _mediatorHandler.PublishEvents(this);

        return success;
    }
}